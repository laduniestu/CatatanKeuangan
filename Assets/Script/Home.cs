using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Home : MonoBehaviour
{
    private List<GrafikMethod> grafikMethod = new List<GrafikMethod>();

    public GameObject grafikPrefab;
    public Transform grafikParent;
    public Text pengeluaran;
    public Text pemasukan;
    private int totalPemasukan = 0;
    private int hasil;
    private int totalPengeluaran = 0;
    Color32 red = new Color32(255,0,0,255);
    Color32 green = new Color32(0,255,0,255);
    
    
    private void Start(){
        //------------- Connecting to Database Cashbook --------------------
        grafikMethod.Clear();
        dbAccess db = GetComponent<dbAccess>();
        db.OpenDB("CashBook.db");


        ArrayList resultMaxNominal = db.SingleSelect("details", "MAX(nominal) as maxNominal");
		
		if(resultMaxNominal.Count > 0)
		{
            hasil =  int.Parse(((string[])resultMaxNominal[0])[0]);
		}
		
        IDataReader reader = db.BasicQuery("SELECT * from details");
        while (reader.Read())
        {
            if(reader.GetString(0).Substring(5, 2)==DateTime.Now.Month.ToString()){

                if(reader.GetString(3)=="pemasukan"){
                    totalPemasukan += reader.GetInt32(1);
                }else{
                    totalPengeluaran += reader.GetInt32(1);
                }
                


                grafikMethod.Add(new GrafikMethod(
                    reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)
                ));
            }
        
        }
        pemasukan.text="Total Pemasukan : " + totalPemasukan.ToString("C0", CultureInfo.CreateSpecificCulture("id-ID"));
        pengeluaran.text="Total Pengeluaran : " + totalPengeluaran.ToString("C0", CultureInfo.CreateSpecificCulture("id-ID"));

        ShowGrafik();
		db.CloseDB();
    }

    private void ShowGrafik(){
        for (int i = 0; i < grafikMethod.Count; i++)
        {
            GameObject tmpObject = Instantiate(grafikPrefab);

            GrafikMethod tmpGrafik = grafikMethod[i];

            tmpObject.GetComponent<ScriptGrafik>().sGrafik(tmpGrafik.Nominal.ToString("C0", CultureInfo.CreateSpecificCulture("id-ID")),tmpGrafik.Keterangan,tmpGrafik.Tanggal,tmpGrafik.Jenis);
            tmpObject.transform.SetParent(grafikParent);
            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            Vector2 rectSize = tmpObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
            float grafikHeight = ((float)tmpGrafik.Nominal/(float)hasil)*170;
            tmpObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(rectSize.x,grafikHeight);
            if(tmpGrafik.Jenis=="pengeluaran"){
                tmpObject.transform.GetChild(0).GetComponent<Image>().color = red;
            }else{
                tmpObject.transform.GetChild(0).GetComponent<Image>().color = green;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Details : MonoBehaviour
{
    private List<DetailsMethod> detailsMethod = new List<DetailsMethod>();
    public GameObject detailsPrefab;
    public Transform detailsParent;
    public Button btnKembali;
    public GameObject details;
    public GameObject notification;
    public GameObject home;
    public Text textNotification;
    Color32 red = new Color32(255,0,0,255);
    // Start is called before the first frame update
    void Start()
    {
        btnKembali.onClick.AddListener(actionKembali);
        detailsMethod.Clear();
        //------------- Connecting to Database Cashbook --------------------
		dbAccess db = GetComponent<dbAccess>();
		db.OpenDB("CashBook.db");

        IDataReader reader = db.BasicQuery("SELECT * FROM details");
        if(reader==null){
            StartCoroutine(Notification(2,"kosong"));
        }
        while (reader.Read())
        {
            detailsMethod.Add(new DetailsMethod(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
        }
        ShowDetails();
		db.CloseDB();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void actionKembali(){
        notification.SetActive(false);
        details.SetActive(false);
        home.SetActive(true);
    }

    private void ShowDetails(){
        for (int i = 0; i < detailsMethod.Count; i++)
        {
            GameObject tmpObject = Instantiate(detailsPrefab);

            DetailsMethod tmpDetails = detailsMethod[i];

            tmpObject.GetComponent<ScriptDetails>().sDetails(tmpDetails.Nominal.ToString(),tmpDetails.Keterangan,tmpDetails.Tanggal,tmpDetails.Jenis);

            tmpObject.transform.SetParent(detailsParent);

            tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    IEnumerator Notification(int timer, string tipe)
    {
        if(tipe=="kosong"){
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Tidak ada data pengeluaran / pemasukan" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
        }
    }
}

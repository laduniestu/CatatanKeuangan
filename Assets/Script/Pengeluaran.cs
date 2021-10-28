using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Pengeluaran : MonoBehaviour
{
    public Button btnSimpan;
    public Button btnKembali;
    public Text tanggal;
    public InputField nominal;
    public InputField keterangan;
    public GameObject notification;
    public GameObject home;
    public GameObject pengeluaran;
    public Text textNotification;
    Color32 red = new Color32(255,0,0,255);
    Color32 green = new Color32(0,255,0,255);
    
    
    private void Start(){
        btnSimpan.onClick.AddListener(actionSimpan);
        btnKembali.onClick.AddListener(actionKembali);
    }

    public void actionSimpan(){
        if(string.IsNullOrEmpty(tanggal.text) || string.IsNullOrEmpty(nominal.text) || string.IsNullOrEmpty(keterangan.text) ){
            StartCoroutine(Notification(2,"kosong"));
        }else{
            //------------- Connecting to Database Cashbook --------------------
            dbAccess db = GetComponent<dbAccess>();
            db.OpenDB("CashBook.db");

            //--------------- Insert into table Pemasukan ----------------------
            string[] pengeluaranInsert = new string[4] {"'"+tanggal.text+"'","'"+nominal.text+"'","'"+keterangan.text+"'","'pengeluaran'"};
			int resultpengeluaranInsert = db.InsertInto("details", pengeluaranInsert);
			if(resultpengeluaranInsert>0){
                StartCoroutine(Notification(2, "sukses"));
                tanggal.text = "";
                nominal.text = "";
                keterangan.text = "";
            }
			else{
                StartCoroutine(Notification(2,"gagal"));
            }

        }
    }

    public void actionKembali(){
        notification.SetActive(false);
        pengeluaran.SetActive(false);
        home.SetActive(true);
        SceneManager.LoadScene("Home");
    }

    IEnumerator Notification(int timer, string tipe)
    {
        if(tipe=="sukses"){
            dbAccess db = GetComponent<dbAccess>();
            db.CloseDB();
            notification.transform.GetChild(0).GetComponent<Image>().color= green;
            textNotification.text= "Sukses menambahkan pengeluaran baru" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            actionKembali();
        }else if(tipe=="gagal"){
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Gagal menambahkan pengeluaran baru" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
        }else{
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Data tidak boleh ada yang kosong" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
        }
    }
}

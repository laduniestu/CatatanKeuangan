using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Pengaturan : MonoBehaviour
{
    public Button btnSimpan;
    public Button btnKembali;
	public Button btnShow;
	public Button btnHide;
	public Button btnShow1;
	public Button btnHide1;
    public InputField passLama;
    public InputField passBaru;
    public GameObject notification;
    public GameObject home;
    public GameObject pengaturan;
    public Text textNotification;
    Color32 red = new Color32(255,0,0,255);
    Color32 green = new Color32(0,255,0,255);
    
    
    private void Start(){
        btnHide.gameObject.SetActive(false);
		btnShow.gameObject.SetActive(false);
        btnHide1.gameObject.SetActive(false);
		btnShow1.gameObject.SetActive(false);
		btnHide.onClick.AddListener(actionHide);
		btnShow.onClick.AddListener(actionShow);
		btnHide1.onClick.AddListener(actionHide1);
		btnShow1.onClick.AddListener(actionShow1);
        btnSimpan.onClick.AddListener(actionSimpan);
        btnKembali.onClick.AddListener(actionKembali);
    }
	public void actionHide(){
		passLama.GetComponent<InputField>().contentType = InputField.ContentType.Password;
		passLama.textComponent.SetAllDirty();
		btnHide.gameObject.SetActive(false);
		btnShow.gameObject.SetActive(true);
	}
	public void actionShow(){
		passLama.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
		passLama.textComponent.SetAllDirty();
		btnHide.gameObject.SetActive(true);
		btnShow.gameObject.SetActive(false);
	}
	public void actionHide1(){
		passBaru.GetComponent<InputField>().contentType = InputField.ContentType.Password;
		passBaru.textComponent.SetAllDirty();
		btnHide.gameObject.SetActive(false);
		btnShow.gameObject.SetActive(true);
	}
	public void actionShow1(){
		passBaru.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
		passBaru.textComponent.SetAllDirty();
		btnHide1.gameObject.SetActive(true);
		btnShow1.gameObject.SetActive(false);
	}
	void Update() {
		if( string.IsNullOrEmpty( passLama.text ))
		{
			btnShow.gameObject.SetActive(false);
		}else{
			btnShow.gameObject.SetActive(true);
		}
		if( string.IsNullOrEmpty( passBaru.text ))
		{
			btnShow1.gameObject.SetActive(false);
		}else{
			btnShow1.gameObject.SetActive(true);
		}
	}

    public void actionSimpan(){
        if(string.IsNullOrEmpty(passLama.text) || string.IsNullOrEmpty(passBaru.text)){
            StartCoroutine(Notification(2,"kosong"));
        }else if(passLama.text == passBaru.text){
            StartCoroutine(Notification(2, "passsama"));
        }else
        {
            //------------- Connecting to Database Cashbook --------------------
            dbAccess db = GetComponent<dbAccess>();
            db.OpenDB("CashBook.db");

            //--------------- Insert into table Pemasukan ----------------------
            ArrayList resultCheckAccount = db.SingleSelectWhere("account", "*", "username", "=", "'user' AND password='"+passLama.text+"'");
			if(resultCheckAccount.Count>0){
                string[] colToUpdate = new string[1] {"password"};
                string[] updateValue = new string[1] {"'"+passBaru.text+"'"};
                bool updateResult = db.UpdateTable("account", colToUpdate,updateValue, "username","'user'");
                if (updateResult==true)
                {
                    StartCoroutine(Notification(2,"sukses"));
                    passBaru.text = "";
                    passLama.text = "";
                }else{
                    StartCoroutine(Notification(2,"gagal"));
                }
            }
			else{
                StartCoroutine(Notification(2,"passlamasalah"));
            }
        }
    }

    public void actionKembali(){
        notification.SetActive(false);
        pengaturan.SetActive(false);
        home.SetActive(true);
    }

    IEnumerator Notification(int timer, string tipe)
    {
        if(tipe=="sukses"){
            dbAccess db = GetComponent<dbAccess>();
            db.CloseDB();
            notification.transform.GetChild(0).GetComponent<Image>().color= green;
            textNotification.text= "Sukses mengganti password" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
            pengaturan.SetActive(false);
            home.SetActive(true);
        }else if(tipe=="passlamasalah"){
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Anda salah memasukkan password saat ini" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
        }else if(tipe=="passsama"){
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Password lama dan password baru harus beda" ;
            notification.SetActive(true);
            yield return new WaitForSeconds(timer);
            notification.SetActive(false);
        }else if(tipe=="gagal"){
            notification.transform.GetChild(0).GetComponent<Image>().color= red;
            textNotification.text= "Gagal mengganti password. Terjadi kesalahan sistem" ;
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

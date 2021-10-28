using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using System;

using System.IO;

public class Login: MonoBehaviour {

	public Button btnLogin;
	public Button btnShow;
	public Button btnHide;
	public InputField username;
	public InputField password;
	public GameObject notification;
	
	
	void Start() {
		btnHide.gameObject.SetActive(false);
		btnShow.gameObject.SetActive(false);
		btnHide.onClick.AddListener(actionHide);
		btnShow.onClick.AddListener(actionShow);
		//------------- Connecting to Database Cashbook --------------------
		dbAccess db = GetComponent<dbAccess>();
		db.OpenDB("CashBook.db");

		//------------- Create Table Cash Book Details --------------------
		string[] detailsCol = new string[4] { "tanggal", "nominal", "keterangan", "jenis" };
		string[] detailsColType = new string[4] { "text", "int", "text", "VARCHAR(10)" };
		bool resultDetails = db.CreateTable("details", detailsCol, detailsColType);
		if(resultDetails == true){Debug.Log("Create Table Details Success");}
		else{Debug.Log("Create Table Details Failed");}

		//------------------- Create Table Account -------------------------
		string[] accountCol = new string[2] { "username", "password"};
		string[] accountColType = new string[2] { "VARCHAR(50) PRIMARY KEY", "VARCHAR(50)"};
		bool resultAccount = db.CreateTable("account", accountCol, accountColType);
		if(resultAccount == true){Debug.Log("Create Table Account Success");}
		else{Debug.Log("Create Table Cash Book Failed");}
		
		//------------------- Insert New Account -------------------------
		ArrayList result = db.SingleSelectWhere("account", "*", "username", "=", "'user'");
		if(result.Count > 0){
			Debug.Log("Account user already registered");
		}else{
			string[] userAccount = new string[2] {"'user'", "'user'"};
			int resultInsertAccount = db.InsertInto("account", userAccount);
			if(resultInsertAccount>0){Debug.Log("Create Account user berhasil");}
			else{Debug.Log("Create account user gagal");}
		}
		db.CloseDB();
		btnLogin.onClick.AddListener(actionLogin);
	}

	public void actionLogin(){

		//------------- Connecting to Database Cashbook --------------------
		dbAccess db = GetComponent<dbAccess>();
		db.OpenDB("CashBook.db");

		//------------- Login Process. Search on Database --------------------
		ArrayList resultLogin = db.SingleSelectWhere("account", "*", "username", "=", "'"+username.text +"' AND password='"+password.text+"'");
		if(resultLogin.Count > 0){
			SceneManager.LoadScene("Home");
		}else{ StartCoroutine(Notification(3)); }
		db.CloseDB();
	}
	
	IEnumerator Notification(int timer)
	{
		notification.SetActive(true);
		yield return new WaitForSeconds(timer);
		notification.SetActive(false);
	}

	public void actionHide(){
		password.GetComponent<InputField>().contentType = InputField.ContentType.Password;
		password.textComponent.SetAllDirty();
		btnHide.gameObject.SetActive(false);
		btnShow.gameObject.SetActive(true);
	}
	public void actionShow(){
		password.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
		password.textComponent.SetAllDirty();
		btnHide.gameObject.SetActive(true);
		btnShow.gameObject.SetActive(false);
	}
	void Update() {
		if( string.IsNullOrEmpty( password.text ))
		{
			btnShow.gameObject.SetActive(false);
		}else{
			btnShow.gameObject.SetActive(true);
		}
	}
}
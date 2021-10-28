using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;

public class dbAccess : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OpenDB(string p)
	{
		Debug.Log("Call to OpenDB:" + p);
		// check if file exists in Application.persistentDataPath
		string filepath = Application.persistentDataPath + "/" + p;
		if(!File.Exists(filepath))
		{
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" + Application.dataPath + "!/assets/" + p);
		}
		
		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}
	
	public void CloseDB(){
		reader.Close(); // clean everything up
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}
	
	public IDataReader BasicQuery(string query){ // run a basic Sqlite query
		Debug.Log(query);
		dbcmd = dbcon.CreateCommand(); // create empty command
		dbcmd.CommandText = query; // fill the command
		reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		return reader; // return the reader
	
	}
	
	
	public bool CreateTable(string name,string[] col, string[] colType){ // Create a table, name, column array, column type array
		string query;
		query  = "CREATE TABLE IF NOT EXISTS " + name + "(" + col[0] + " " + colType[0];
		for(var i=1; i< col.Length; i++){
			query += ", " + col[i] + " " + colType[i];
		}
		query += ")";
		try{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return false;
		}
		return true;
	}
	
	
	public bool UpdateTable(string name,string[] col, string[] colVal, string val1, string val2){ // Create a table, name, column array, column type array
		string query;
		query  = "UPDATE " + name + " SET " + col[0] + " = " + colVal[0];
		for(var i=1; i< col.Length; i++){
			query += ", " + col[i] + " = " + colVal[i];
		}
		query += " WHERE " + val1 + "=" + val2;
		Debug.Log(query);
		try{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return false;
		}
		return true;
	}
	
	public int InsertIntoSingle(string tableName, string colName , string value ){ // single insert
		string query;
		query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
		try
		{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public int InsertIntoSpecific(string tableName, string[] col, string[] values){ // Specific insert with col and values
		string query;
		query = "INSERT INTO " + tableName + "(" + col[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + col[i];
		}
		query += ") VALUES (" + values[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		Debug.Log(query);
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	
	public int InsertInto(string tableName , string[] values ){ // basic Insert with just values
		string query;
		query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for(int i=1; i< values.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		Debug.Log(query);
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	public ArrayList SingleSelect(string tableName , string itemToSelect){ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName;	
		Debug.Log(query);
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				j++;
			}
			readArray.Add(row);
		}
		// Debug.Log(readArray);
		return readArray; // return matches
	}
	
	public ArrayList SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;	
		Debug.Log(query);
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				// Debug.Log(row[j]);
				j++;
			}
			readArray.Add(row);
		}
		return readArray; // return matches
	}

	public ArrayList SelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue){ // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;	
		Debug.Log(query);
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		while(reader.Read()){
			readArray.Add(reader[0]);
		}
		return readArray; // return matches
	}

	
	public ArrayList SelectDetails(string jenis){ // Selects a single Item
		string query;
		query = "SELECT nominal FROM details WHERE jenis = '" + jenis + "'";	
		Debug.Log(query);
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		ArrayList readArray = new ArrayList();
		while(reader.Read()){
			readArray.Add(reader[0]);
		}
		return readArray; // return matches
	}

	// Update is called once per frame
	void Update () {
	
	}
}
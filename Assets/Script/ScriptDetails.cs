using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptDetails : MonoBehaviour
{
    public GameObject In_;
    public GameObject Out_;
    public GameObject Nominal_;
    public GameObject Keterangan_;
    public GameObject Tanggal_;

    public void sDetails(string nominal, string keterangan, string tanggal, string jenis){
        if (jenis == "pengeluaran")
        {   
            string template = "[ - ] {0}";
            this.Nominal_.GetComponent<Text>().text = string.Format(template, nominal);
            this.In_.SetActive(false);
            this.Out_.SetActive(true);
        }else{
            string template = "[ + ] {0}";
            this.Nominal_.GetComponent<Text>().text = string.Format(template, nominal);
            this.In_.SetActive(true);
            this.Out_.SetActive(false);
        }
        this.Keterangan_.GetComponent<Text>().text = keterangan;
        this.Tanggal_.GetComponent<Text>().text = tanggal;
    }
}

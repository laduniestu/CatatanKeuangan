using System.Collections;
using System.Collections.Generic;
using UnityEngine; using System;
using UnityEngine.UI;

public class ScriptGrafik : MonoBehaviour
{
    public GameObject Bar;
    public GameObject nominalText;
    public GameObject tanggalText;

    public void sGrafik(string nominal, string keterangan, string tanggal, string jenis){
        // if (jenis == "pengeluaran")
        // {   
        //     string template = "[ - ] {0}";
        //     this.Nominal_.GetComponent<Text>().text = string.Format(template, nominal);
        //     this.In_.SetActive(false);
        //     this.Out_.SetActive(true);
        // }else{
        //     string template = "[ + ] {0}";
        //     this.Nominal_.GetComponent<Text>().text = string.Format(template, nominal);
        //     this.In_.SetActive(true);
        //     this.Out_.SetActive(false);
        // }
        string[] tanggalSliced =  tanggal.Split(char.Parse("-"));
        this.nominalText.GetComponent<Text>().text = nominal;
        this.tanggalText.GetComponent<Text>().text = tanggalSliced[2].Substring(0, 2);

        
        // float size = Bar.GetComponent<Renderer> ().bounds.size.y;

        // Vector3 rescale = Bar.transform.localScale;

        // rescale.y = 132 * rescale.y / size;

        // this.transform.position.y += 12;
        // Bar.sizeDelta = new Vector2(50, 50);
    }
}

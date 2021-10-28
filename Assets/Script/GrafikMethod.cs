using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

class GrafikMethod{
    public string Tanggal {get; set;}

    public int Nominal {get; set;}

    public string Keterangan {get; set;}

    public string Jenis {get; set;}

    public GrafikMethod(string tanggal, int nominal, string keterangan, string jenis){
        this.Tanggal = tanggal;
        this.Nominal =  nominal;
        this.Keterangan = keterangan;
        this.Jenis = jenis;
    }
}
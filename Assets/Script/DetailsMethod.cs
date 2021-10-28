using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;

class DetailsMethod{
    public string Tanggal {get; set;}

    public string Nominal {get; set;}

    public string Keterangan {get; set;}

    public string Jenis {get; set;}

    public DetailsMethod(string tanggal, int nominal, string keterangan, string jenis){
        System.DateTime dateTime = System.DateTime.Parse(tanggal);
        this.Tanggal = dateTime.ToString("dd-MM HH:mm");
        this.Nominal =  nominal.ToString("C0", CultureInfo.CreateSpecificCulture("id-ID"));
        this.Keterangan = keterangan;
        this.Jenis = jenis;
    }
}
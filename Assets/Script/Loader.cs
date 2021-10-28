using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public Button btnPemasukan;
    public Button btnPengeluaran;
    public Button btnDetailCash;
    public Button btnPengaturan;
    public GameObject componentHome;
    public GameObject componenetPemasukan;
    public GameObject componenetPengeluaran;
    public GameObject componenetDetailCash;
    public GameObject componenetPengaturan;
    public GameObject componenetNotification;
    
    private void Start(){
        componentHome.SetActive(true);
        componenetPengeluaran.SetActive(false);
        componenetDetailCash.SetActive(false);
        componenetPengaturan.SetActive(false);
        componenetPemasukan.SetActive(false);
        componenetNotification.SetActive(false);
        btnPemasukan.onClick.AddListener(actionPemasukan);
        btnPengeluaran.onClick.AddListener(actionPengeluaran);
        btnPengaturan.onClick.AddListener(actionPengaturan);
        btnDetailCash.onClick.AddListener(actionDetail);
    }

    public void actionPemasukan(){
        componenetPengeluaran.SetActive(false);
        componenetDetailCash.SetActive(false);
        componenetPengaturan.SetActive(false);
        componentHome.SetActive(false);
        componenetPemasukan.SetActive(true);
    }

    public void actionPengeluaran(){
        componenetPemasukan.SetActive(false);
        componenetDetailCash.SetActive(false);
        componenetPengaturan.SetActive(false);
        componentHome.SetActive(false);
        componenetPengeluaran.SetActive(true);
    }

    public void actionDetail(){
        componenetPemasukan.SetActive(false);
        componenetPengeluaran.SetActive(false);
        componenetPengaturan.SetActive(false);
        componentHome.SetActive(false);
        componenetDetailCash.SetActive(true);
    }
    public void actionPengaturan(){
        componenetPemasukan.SetActive(false);
        componenetPengeluaran.SetActive(false);
        componenetPengaturan.SetActive(true);
        componentHome.SetActive(false);
        componenetDetailCash.SetActive(false);
    }

    public void actionHome(){
        componenetPengeluaran.SetActive(false);
        componenetDetailCash.SetActive(false);
        componenetPengaturan.SetActive(false);
        componenetPemasukan.SetActive(false);
        componentHome.SetActive(true);
    }

}

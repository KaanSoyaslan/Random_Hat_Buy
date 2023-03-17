using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelevect : MonoBehaviour
{

    public int hatrnd;
    public Button[] butonlar;
    public GameObject[] Si; //soru iþareti
    public bool[] þapkalar;

    public Button ÞapkaSatýnALbtn;
    public GameObject hatSelectScreen;


    public Image HatImaege;
    public Image HatBTNImaege;
    public Sprite[] HatImaegeALL;
    public GameObject HatShowScreen;

    void Start()
    {
        for (int i = 0; i < þapkalar.Length; i++)
        {
            þapkalar[i] = false;
            butonlar[i].interactable = false;
            Si[i].SetActive(true);

        }




        PlayerPrefs.SetInt("hat0buy", 1); //ilk þapka açýk (boþ)

        hatSelectScreen.SetActive(false);
        HatShowScreen.SetActive(false);
    }

    void Update()
    {

        HatBTNImaege.sprite = HatImaegeALL[PlayerPrefs.GetInt("selectedHat")]; //seçilmiþ þapkayý edin


        SoruIsaretiGoster(); //bize çýkmayan þapkalar soru iþaretli
        SapkaButonGoster(); //çýkan þapkalarý seçme butonu aktif  et
        SatýnAlmaButonAktiflik(); //tüm þapkalarý aldýktan sonra kapat
    }
    public void SatýnAlmaButonAktiflik()
    {
        int toplamSapka = 0;
        for (int i = 0; i < þapkalar.Length; i++)
        {
            if (þapkalar[i] == true)
            {
                toplamSapka++;
            }
        }
 
        if (toplamSapka++ == þapkalar.Length) // boþ þapkayýda ekledim
        {
            ÞapkaSatýnALbtn.interactable = false;
        }
    }
    public void SoruIsaretiGoster()
    {
        for (int i = 0; i < Si.Length; i++)
        {
            if (PlayerPrefs.GetInt("hat" + i + "buy") == 1)
            {
                þapkalar[i] = true;
                Si[i].SetActive(false);
            }
        }
    }
    public void SapkaButonGoster()
    {
        for (int i = 0; i < butonlar.Length; i++)
        {
            if (PlayerPrefs.GetInt("hat" + i + "buy") == 1 && PlayerPrefs.GetInt("selectedHat") != i)
            {

                butonlar[i].interactable = true;

            }
        }
    }

    public void RandomHat() //rastgele þapka ver
    {
        if (PlayerPrefs.GetInt("coin") >= 100)
        {

            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 100);

        HatSelect:
            hatrnd = Random.Range(1, HatImaegeALL.Length);
            Debug.Log(hatrnd);
            if (þapkalar[hatrnd] == false)  //varmý yokmu kontrol
            {
                for (int i = 1; i < HatImaegeALL.Length + 1; i++)//max deðeri randomun max deðeri
                {
                    if (hatrnd == i)
                    {
                        PlayerPrefs.SetInt("hat" + hatrnd + "buy", 1);
                        StartCoroutine(HatShoww(hatrnd));
                    }

                }
            }
            else
            {
                goto HatSelect;

            }
        }
        else
        {
            SoundManager.PlaySound("none");
        }

    }


    public void Hatselect(int num)
    {
        PlayerPrefs.SetInt("selectedHat", num);
        butonlar[num].interactable = false;


    }
    public void HatbuyMenuAC()
    {
        hatSelectScreen.SetActive(true);
    }



    public void ResetAll()
    {
        PlayerPrefs.SetInt("selectedHat", 0);
        for (int i = 1; i < þapkalar.Length; i++)
        {
            PlayerPrefs.SetInt("hat" + i + "buy", 0);
        }

    }
    public void ResetAllnegative()
    {
        PlayerPrefs.SetInt("selectedHat", 0);
        for (int i = 1; i < þapkalar.Length; i++)
        {
            PlayerPrefs.SetInt("hat" + i + "buy", 1);
        }

    }

    IEnumerator HatShoww(int i) //þapka çýktýðýnda bildirim olarak göster
    {
        HatImaege.sprite = HatImaegeALL[i];
        HatShowScreen.SetActive(true);
        // HatShow[i].SetActive(true);
        SoundManager.PlaySound("finish");
        yield return new WaitForSeconds(3f);
        //HatShow[i].SetActive(false);
        HatShowScreen.SetActive(false);
    }
    public void HatSelectKapa()
    {
        hatSelectScreen.SetActive(false);
    }
}

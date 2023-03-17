using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatSelevect : MonoBehaviour
{

    public int hatrnd;
    public Button[] butonlar;
    public GameObject[] Si; //soru i�areti
    public bool[] �apkalar;

    public Button �apkaSat�nALbtn;
    public GameObject hatSelectScreen;


    public Image HatImaege;
    public Image HatBTNImaege;
    public Sprite[] HatImaegeALL;
    public GameObject HatShowScreen;

    void Start()
    {
        for (int i = 0; i < �apkalar.Length; i++)
        {
            �apkalar[i] = false;
            butonlar[i].interactable = false;
            Si[i].SetActive(true);

        }




        PlayerPrefs.SetInt("hat0buy", 1); //ilk �apka a��k (bo�)

        hatSelectScreen.SetActive(false);
        HatShowScreen.SetActive(false);
    }

    void Update()
    {

        HatBTNImaege.sprite = HatImaegeALL[PlayerPrefs.GetInt("selectedHat")]; //se�ilmi� �apkay� edin


        SoruIsaretiGoster(); //bize ��kmayan �apkalar soru i�aretli
        SapkaButonGoster(); //��kan �apkalar� se�me butonu aktif  et
        Sat�nAlmaButonAktiflik(); //t�m �apkalar� ald�ktan sonra kapat
    }
    public void Sat�nAlmaButonAktiflik()
    {
        int toplamSapka = 0;
        for (int i = 0; i < �apkalar.Length; i++)
        {
            if (�apkalar[i] == true)
            {
                toplamSapka++;
            }
        }
 
        if (toplamSapka++ == �apkalar.Length) // bo� �apkay�da ekledim
        {
            �apkaSat�nALbtn.interactable = false;
        }
    }
    public void SoruIsaretiGoster()
    {
        for (int i = 0; i < Si.Length; i++)
        {
            if (PlayerPrefs.GetInt("hat" + i + "buy") == 1)
            {
                �apkalar[i] = true;
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

    public void RandomHat() //rastgele �apka ver
    {
        if (PlayerPrefs.GetInt("coin") >= 100)
        {

            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") - 100);

        HatSelect:
            hatrnd = Random.Range(1, HatImaegeALL.Length);
            Debug.Log(hatrnd);
            if (�apkalar[hatrnd] == false)  //varm� yokmu kontrol
            {
                for (int i = 1; i < HatImaegeALL.Length + 1; i++)//max de�eri randomun max de�eri
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
        for (int i = 1; i < �apkalar.Length; i++)
        {
            PlayerPrefs.SetInt("hat" + i + "buy", 0);
        }

    }
    public void ResetAllnegative()
    {
        PlayerPrefs.SetInt("selectedHat", 0);
        for (int i = 1; i < �apkalar.Length; i++)
        {
            PlayerPrefs.SetInt("hat" + i + "buy", 1);
        }

    }

    IEnumerator HatShoww(int i) //�apka ��kt���nda bildirim olarak g�ster
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

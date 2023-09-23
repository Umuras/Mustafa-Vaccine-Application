using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.AndroidBuildSystem;

public class ArrayOrderChange : MonoBehaviour
{
    public TextMeshProUGUI vaccineText;
    public int counter;
    private string dateString = "22:07";
    public bool changeTime = true;
    public TextMeshProUGUI clockText;

    public string[] vaccineArray = { "SAĞ KOL", "SOL KOL", "SAĞ BACAK", "SOL BACAK" };
    void Start()
    {
        counter = PlayerPrefs.GetInt("counterNum"); // Burada sayaç değerini uygulama başlatılınca çağırıyoruz çünkü uygulama açıldığı en son hangi dizi elemanında kalmış bu sayaçtaki değer belirliyor.
        //vaccineText.text = PlayerPrefs.GetString("saveVaccinePart"); //Burada en son kalan dizi elemanının değerini yazdırıyoruz.
        vaccineText.text = vaccineArray[counter];
    }

    
    void Update()
    {
        VaccineArrayChange();
        Application.runInBackground = true;
    }
    

    public void VaccineArrayChange()
    {
        
        if (DateTime.Now.ToString("HH:mm")  == dateString && changeTime) //DateTime.Now.ToString("HH:mm") diyerek 24 Hour saati biçiminde saat ve dakika olarak string şeklinde bilgisayar saatine erişiyoruz.
        {
            counter++;
           //PlayerPrefs.SetString("saveVaccinePart", vaccineArray[counter]); // Değişim dizi elemanı kaydedildi.
            if (vaccineArray.Length == counter) // Dizi uzunluğu ile sayaç birbirine eşit olursa sayacı sıfırlıyoruz ki dizinin dışına çıkmasın eleman olarak vaccineArray[4] mesela dizi 0 1 2 3 elemandan oluşuyor 4 yazarsa hata verir.
            {
                counter = 0;
            }
            vaccineText.text = vaccineArray[counter]; // Burada texte dizi elemanını yazdırıyoruz.
            PlayerPrefs.SetInt("counterNum", counter); // Değişen counter değeri kaydedildi.
            changeTime = false; //Burada false saat 21.30 olduğunda bir kere dizide ilerleyip durmasını sağlıyoruz.
        }

        clockText.text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString(); // burada texte saat dakika ve saniyeyi yazdırıyoruz.
        if (DateTime.Now.ToString("HH:mm") == "22:08") // Burada saat 21.31 olduğu an bool değişkenini true yaptırarak bir sonraki 21.30 olduğunda dizi elemanının hareket etmesini sağlıyoruz.
        {
            changeTime = true;
        }

    }
}

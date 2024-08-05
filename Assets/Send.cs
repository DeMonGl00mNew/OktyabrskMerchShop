using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;

//СЃРєСЂРёРїС‚, РѕС‚РІРµС‡Р°СЋС‰РёР№ Р·Р° Р»РѕРіРёРєСѓ СЂР°СЃСЃС‹Р»РєРё РїРёСЃРµРј РєР»РёРµРЅС‚Р°Рј.

public class Send : MonoBehaviour
{
    public TMP_Text NazvnieTowara;
    public TMP_Text RazmerTovara;
    public TMP_Text Cena;
    public TMP_Text Alert;
    private string emailKomu= "";
     public string emailSvoy= "oktyabskmerch@gmail.com";
    public string passwordSvoy = "ksrbaytkO";
    private bool couratinIsActive = false;
    //С„СѓРЅРєС†РёСЏ РЅР°СЃС‚СЂРѕР№РєРё Рё РѕС‚РїСЂР°РІРєРё РїРёСЃСЊРјР° Р°РґРјРёРЅРёСЃС‚СЂР°С‚РѕСЂСѓ Рё РєР»РёРµРЅС‚Р°Рј
    public void send()
   {
        
        emailKomu = AuthUser.Instance.CurrentEmail;
        if(!couratinIsActive) StartCoroutine(AlertBuyAndPost());
        SetupMessageAndSend("Р§РµРє Рѕ РїРѕРєСѓРїРєРµ",
            $"Р—Р°РєР°Р· РїСЂРёРЅСЏС‚. РќР°РёРјРµРЅРѕРІР°РЅРёРµ С‚РѕРІР°СЂР°: {NazvnieTowara.text}; СЂР°Р·РјРµСЂ: {RazmerTovara.text}; С†РµРЅР°: {Cena.text}.",
             emailSvoy,
             passwordSvoy,
             emailKomu
            );
        
        SetupMessageAndSend("Р§РµРє Рѕ Р·Р°РєР°Р·Рµ",
    $"РєР»РёРµРЅС‚ {emailKomu}; РЅР°РёРјРµРЅРѕРІР°РЅРёРµ С‚РѕРІР°СЂР°: {NazvnieTowara.text}; СЂР°Р·РјРµСЂ: {RazmerTovara.text}; С†РµРЅР°: {Cena.text}.",
            emailSvoy,
            passwordSvoy,
            emailSvoy
    );
    }
    //С„СѓРЅРєС†РёСЏ РѕС‚РїСЂР°РІРєРё РїРёСЃСЊРјР°
    private void SetupMessageAndSend (string tema, string soderganie,string otkuda, string passwordOtpravitely,string kuda)
    {
        MailMessage message = new MailMessage();
        message.Subject = tema;
        message.Body = soderganie;
        message.From = new MailAddress(otkuda);
        message.To.Add(kuda);
        message.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.Credentials = new NetworkCredential(message.From.Address, passwordOtpravitely );
        client.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
         delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
         { return true; };
        client.Send(message);
    }

    //СЃРѕРїСЂРѕРіСЂР°РјРјР° РґР»СЏ РѕРїРѕРІРµС‰РµРЅРёСЏ РѕР± РѕС‚РїСЂР°РІРєРё РїРёСЃСЊРјР° РїРѕР»СЊР·РѕРІР°С‚РµР»СЋ
    IEnumerator AlertBuyAndPost()
    {
        couratinIsActive = true;
        Alert.gameObject.SetActive(true);
        Alert.text =$"РўРѕРІР°СЂ РєСѓРїР»РµРЅ, РїСЂРѕРІРµСЂСЊС‚Рµ РїРѕС‡С‚Сѓ {emailKomu} ";
        yield return new WaitForSeconds(4f);
        Alert.gameObject.SetActive(false);
        couratinIsActive = false;
    }

}


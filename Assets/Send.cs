using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;

//скрипт, отвечающий за логику рассылки писем клиентам.

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
    //функция настройки и отправки письма администратору и клиентам
    public void send()
   {
        
        emailKomu = AuthUser.Instance.CurrentEmail;
        if(!couratinIsActive) StartCoroutine(AlertBuyAndPost());
        SetupMessageAndSend("Чек о покупке",
            $"Заказ принят. Наименование товара: {NazvnieTowara.text}; размер: {RazmerTovara.text}; цена: {Cena.text}.",
             emailSvoy,
             passwordSvoy,
             emailKomu
            );
        
        SetupMessageAndSend("Чек о заказе",
    $"клиент {emailKomu}; наименование товара: {NazvnieTowara.text}; размер: {RazmerTovara.text}; цена: {Cena.text}.",
            emailSvoy,
            passwordSvoy,
            emailSvoy
    );
    }
    //функция отправки письма
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

    //сопрограмма для оповещения об отправки письма пользователю
    IEnumerator AlertBuyAndPost()
    {
        couratinIsActive = true;
        Alert.gameObject.SetActive(true);
        Alert.text =$"Товар куплен, проверьте почту {emailKomu} ";
        yield return new WaitForSeconds(4f);
        Alert.gameObject.SetActive(false);
        couratinIsActive = false;
    }

}

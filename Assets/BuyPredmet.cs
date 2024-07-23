using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;

/*скрипт покупки товара, активации отсылки на почту, обновления количества валюты в БД;*/
public class BuyPredmet : MonoBehaviour
{
    static public int StoymostVeshi;
    public TMP_Text DengiText;
    public Send EmailSendManger;

    // функция запуска сопрограммы денежной транзакции при нажатии кнопки купить
    public void BuyOnClick()
    {
        StartCoroutine(RefreshMoneyCouratine(AuthUser.Instance.CurrentLogin, AuthUser.Instance.CurrentPassword));
    }
    // сопрограмма денежной транзакции и обновление информации на сервере в БД
    public IEnumerator RefreshMoneyCouratine(string login, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("pass", pass);


        UnityWebRequest www = UnityWebRequest.Post("http://eisk1848.ru/bdMerch.php ", form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        www.Dispose();
        if (data != "неверный логин или пароль")
        {
            data = data.Remove(data.Length - 1);
            AuthUser.Instance.CurrentMoney = Convert.ToInt32(AuthUser.Instance.GetValue(data, "Money:"));
            MoneyMinusStoymost();
        }

    }
    // функция вычисления стоимости предмета и отправка электронного письма
    private void MoneyMinusStoymost()
    {
        if (AuthUser.Instance.CurrentMoney >= StoymostVeshi)
        {
            EmailSendManger.send();
            AuthUser.Instance.CurrentMoney -= StoymostVeshi;
            StartCoroutine((AuthUser.Instance.UserBuyCouratine(AuthUser.Instance.ID, AuthUser.Instance.CurrentMoney)));
            DengiText.text = AuthUser.Instance.CurrentMoney.ToString();
        }

    }
}

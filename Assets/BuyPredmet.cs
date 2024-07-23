using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;

/*������ ������� ������, ��������� ������� �� �����, ���������� ���������� ������ � ��;*/
public class BuyPredmet : MonoBehaviour
{
    static public int StoymostVeshi;
    public TMP_Text DengiText;
    public Send EmailSendManger;

    // ������� ������� ����������� �������� ���������� ��� ������� ������ ������
    public void BuyOnClick()
    {
        StartCoroutine(RefreshMoneyCouratine(AuthUser.Instance.CurrentLogin, AuthUser.Instance.CurrentPassword));
    }
    // ����������� �������� ���������� � ���������� ���������� �� ������� � ��
    public IEnumerator RefreshMoneyCouratine(string login, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("pass", pass);


        UnityWebRequest www = UnityWebRequest.Post("http://eisk1848.ru/bdMerch.php ", form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        www.Dispose();
        if (data != "�������� ����� ��� ������")
        {
            data = data.Remove(data.Length - 1);
            AuthUser.Instance.CurrentMoney = Convert.ToInt32(AuthUser.Instance.GetValue(data, "Money:"));
            MoneyMinusStoymost();
        }

    }
    // ������� ���������� ��������� �������� � �������� ������������ ������
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

using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;
/*скрипт авторизации пользователя, и отправка формы к БД*/
public class AuthUser : MonoBehaviour
{
    public GameObject AdminPanel;
    static public AuthUser Instance { get; private set; }
    public TMP_InputField loginInputField, passInputField;
    public TMP_Text Alert;
    public GameObject SotridnikPanel;
    public GameObject EnterPanel;
    public string ID;
    public string CurrentLogin;
    public string CurrentPassword;
    public int CurrentMoney;
    public string CurrentEmail;

    public bool adminBD=false;
    // паттерн Singletone для инициализации зависимостей
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // функция валидации и вызов сопрограммы авторизация пользователя при нажатии на кнопку войти
    public void ButtonClick()
    {
        if (adminBD)
        { if(loginInputField.text == "admin" && passInputField.text == "admin")
            {
                EnterPanel.SetActive(false);
                AdminPanel.SetActive(true);
                Alert.text = "";
            }
          else
            {
                Alert.text ="Этот пользователь не имеет прав доступа, как администратор БД";
            }
        }
        else
        if (loginInputField.text!="" && passInputField.text!="") 
        {
            StartCoroutine(AuthUserCouratine(loginInputField.text, passInputField.text));
        }


    }
    // сопрограмма авторизации пользователя  и отсылка данных аутентификации на сервер eisk1848.ru
    // ожидание авторизации и переход в панель сотрудников
    public IEnumerator AuthUserCouratine(string login, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("pass", pass);
        

        UnityWebRequest www = UnityWebRequest.Post("https://eisk1848.ru/bdMerch.php ",form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        www.Dispose();

        if (data != "неверный логин или пароль")
        {
            data = data.Remove(data.Length - 1);
            CurrentLogin = loginInputField.text;
            CurrentPassword = passInputField.text;
            ID = GetValue(data, "ID:");
            CurrentMoney = Convert.ToInt32(GetValue(data, "Money:"));
            CurrentEmail = GetValue(data, "Email:");
            EnterPanel.SetActive(false);
            SotridnikPanel.SetActive(true);
            ShopLogic.Instance.LoadFavorites();
        }
        else 
        {
            Alert.text = data;
        }



    }


    // сопрограмма полуения информации о денежных средствах с сервера
    public IEnumerator UserBuyCouratine(string id, int money)
    {
        WWWForm form = new WWWForm();
        form.AddField("IDFromUnity", id);
        form.AddField("MoneyFromMoney", money);

        UnityWebRequest www = UnityWebRequest.Post("http://eisk1848.ru/bdMerchMoney.php ", form);
        yield return www.SendWebRequest();
        www.Dispose();
    }
    // функция интерпретации ответа с сервера - разбиение по рапзделителю строки
    public string GetValue(string data, string index)
    {
        string val = data.Substring(data.IndexOf(index) + index.Length);
        if (val.Contains("|"))
            val = val.Remove(val.IndexOf("|"));
        return val;
    }
    // функция активации опции администрирования
    public void adminBDToggle(bool isAdmin)
    {
        adminBD = isAdmin;
    }
}

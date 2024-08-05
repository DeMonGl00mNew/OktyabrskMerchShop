using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

/*скрипт отвечающий за администрирование БД и отправка формы к БД;*/
public class AdministrateBD : MonoBehaviour
{
    public GameObject AdminPanel;
    public GameObject EnterPanel;
    public TMP_InputField logintField, passField, emailField, CountField, AdminEmailField, AdminPassworField;
    public TMP_Text Alert;
    public Send SendScript;

    //валидация и запуск сопрограммы добавления пользователя
    public void AddClick()
    {
        if (logintField.text != "" && passField.text != "" &&
            emailField.text != "" && CountField.text != "")
        {
            StartCoroutine(AddUserCouratine());
        }
    }
    //валидация и запуск сопрограммы добавления электронной почты
    public void AdminEmailPassClickOnly()
    {
        if (AdminEmailField.text != "" && AdminPassworField.text != "" && logintField.text == "")
        {
            SendScript.emailSvoy = AdminEmailField.text;
            SendScript.passwordSvoy = AdminPassworField.text;
            Alert.text = $"Почта отправки чеков изменена на {AdminEmailField.text}";
        }
    }
    //выключение панели администрирование и влючение панели ввода пользователя
    public void Exit()
    {
        EnterPanel.SetActive(true);
        AdminPanel.SetActive(false);
    }
    //сопрограмма добавления пользователя через bdMerchReg.php на сервере  eisk1848.ru в БД
    IEnumerator AddUserCouratine()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", logintField.text);
        form.AddField("pass", passField.text);
        form.AddField("mail", emailField.text);
        form.AddField("count", CountField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://eisk1848.ru/bdMerchReg.php ", form);
        yield return www.SendWebRequest();
        Alert.text = $"Пользователь {logintField.text} добавлен в БД";
        www.Dispose();
    }
}

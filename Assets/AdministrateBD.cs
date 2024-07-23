using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

/*������ ���������� �� ����������������� �� � �������� ����� � ��;*/
public class AdministrateBD : MonoBehaviour
{
    public GameObject AdminPanel;
    public GameObject EnterPanel;
    public TMP_InputField logintField, passField, emailField, CountField, AdminEmailField, AdminPassworField;
    public TMP_Text Alert;
    public Send SendScript;

    //��������� � ������ ����������� ���������� ������������
    public void AddClick()
    {
        if (logintField.text != "" && passField.text != "" &&
            emailField.text != "" && CountField.text != "")
        {
            StartCoroutine(AddUserCouratine());
        }
    }
    //��������� � ������ ����������� ���������� ����������� �����
    public void AdminEmailPassClickOnly()
    {
        if (AdminEmailField.text != "" && AdminPassworField.text != "" && logintField.text == "")
        {
            SendScript.emailSvoy = AdminEmailField.text;
            SendScript.passwordSvoy = AdminPassworField.text;
            Alert.text = $"����� �������� ����� �������� �� {AdminEmailField.text}";
        }
    }
    //���������� ������ ����������������� � �������� ������ ����� ������������
    public void Exit()
    {
        EnterPanel.SetActive(true);
        AdminPanel.SetActive(false);
    }
    //����������� ���������� ������������ ����� bdMerchReg.php �� �������  eisk1848.ru � ��
    IEnumerator AddUserCouratine()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", logintField.text);
        form.AddField("pass", passField.text);
        form.AddField("mail", emailField.text);
        form.AddField("count", CountField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://eisk1848.ru/bdMerchReg.php ", form);
        yield return www.SendWebRequest();
        Alert.text = $"������������ {logintField.text} �������� � ��";
        www.Dispose();
    }
}

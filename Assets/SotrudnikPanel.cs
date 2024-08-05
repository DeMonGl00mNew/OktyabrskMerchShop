using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// скрипт вывода денег клиента при запуске приложения
public class SotrudnikPanel : MonoBehaviour
{
    public TMP_Text DengiText;
    private void OnEnable()
    {
        DengiText.text = AuthUser.Instance.CurrentMoney.ToString();
    }



}

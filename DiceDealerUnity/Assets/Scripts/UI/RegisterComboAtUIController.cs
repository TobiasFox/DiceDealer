using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterComboAtUIController : MonoBehaviour
{
    void Start()
    {
        var uiController = FindObjectOfType<UIController>();
        uiController.SetComboTextField(GetComponent<TextMeshProUGUI>());
    }

}

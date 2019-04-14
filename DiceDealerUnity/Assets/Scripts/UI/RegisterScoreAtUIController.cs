using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScoreAtUIController : MonoBehaviour
{
    void Start()
    {
        var uiController = FindObjectOfType<UIController>();
        uiController.SetScoreTextField(GetComponent<TextMeshProUGUI>());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterStatsAtUIController : MonoBehaviour
{
    void Start()
    {
        var uiController = FindObjectOfType<UIController>();
        uiController.statisticsPanel = gameObject;
    }

}

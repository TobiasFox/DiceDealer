﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUIElement : MonoBehaviour
{
    void Start()
    {
        var uiController = FindObjectOfType<UIController>();
        uiController.SetScoreTextField(GetComponent<Text>());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour
{

    [SerializeField] private GameObject aboutPanel;
    public void ChangeAboutPanelVisibility()
    {
        aboutPanel.SetActive(!aboutPanel.activeSelf);
    }
        
}

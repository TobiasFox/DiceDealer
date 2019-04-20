using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchVisibility : MonoBehaviour
{
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private bool switchAboutPanel;

    public void SwitchVisibilityOfGameObject()
    {
        if (switchAboutPanel)
        {
            aboutPanel.SetActive(!aboutPanel.activeSelf);
            creditsPanel.SetActive(false);
        }
        else
        {
            creditsPanel.SetActive(!creditsPanel.activeSelf);
            aboutPanel.SetActive(false);
        }
    }
}
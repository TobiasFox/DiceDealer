using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex = 1;
    private void Awake()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);
    }
}

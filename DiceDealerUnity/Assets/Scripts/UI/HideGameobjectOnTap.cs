using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideGameobjectOnTap : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || IsTouched())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsTouched()
    {
        var touchCount = Input.touchCount > 0;
        if (!touchCount)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        return !EventSystem.current.currentSelectedGameObject && touch.phase == TouchPhase.Ended;
    }
}
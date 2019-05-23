using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowHint : MonoBehaviour
{
    [SerializeField] private float displaySpeed = 0.3f;
    [SerializeField] private float startWaitTime = 1.5f;

    private Image[] arrows;
    private int index;

    private void Start()
    {
        arrows = GetComponentsInChildren<Image>();
        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }

        StartCoroutine(ShowHint());
    }

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(startWaitTime);

        while (true)
        {
            yield return FloatingArrows();
        }
    }

    private IEnumerator FloatingArrows()
    {
        arrows[index == 0 ? arrows.Length - 1 : index - 1].gameObject.SetActive(false);

        arrows[index].gameObject.SetActive(true);
        index = (index + 1) % arrows.Length;
        yield return new WaitForSeconds(displaySpeed);
    }

    private IEnumerator IterativeArrows()
    {
        if (index >= arrows.Length)
        {
            foreach (var arrow in arrows)
            {
                arrow.gameObject.SetActive(false);
            }

            index = 0;
            yield return new WaitForSeconds(displaySpeed * 1.5f);
        }

        arrows[index].gameObject.SetActive(true);
        index++;

        yield return new WaitForSeconds(displaySpeed);
    }
}
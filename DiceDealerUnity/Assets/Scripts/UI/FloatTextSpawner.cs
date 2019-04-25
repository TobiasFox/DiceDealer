using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FloatTextSpawner : MonoBehaviour
{
    public GameObject AnimatedFloatingTextPrefab;

    public void SpawnFloatingText(string text, Vector2 screenPosition, float size)
    {
        //TODO use object pooler here
        GameObject floatText = Instantiate(AnimatedFloatingTextPrefab, gameObject.transform);
        floatText.GetComponentInChildren<TMP_Text>().text = text;
        floatText.GetComponent<RectTransform>().position = screenPosition;
        floatText.transform.localScale = Vector3.one * size;
        Destroy(floatText, 1);
    }

    internal void SpawnFloatingTextAfterTime(string text, Vector2 screenPos, int scale, float timeToSpawnFloatText)
    {
        StartCoroutine(SpawnFloatingTextAfterTimeCoroutine(text, screenPos, scale, timeToSpawnFloatText));
    }

    IEnumerator SpawnFloatingTextAfterTimeCoroutine(string text, Vector2 screenPosition, int v, float timeToSpawnFloatText)
    {
        yield return new WaitForSeconds(timeToSpawnFloatText);
        SpawnFloatingText(text, screenPosition, v);
    }
}

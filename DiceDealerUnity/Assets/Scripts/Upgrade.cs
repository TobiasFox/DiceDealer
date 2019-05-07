using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string name;
    public int price;
    public float upgradeMultiplier;
    public float priceMultiplier;

}
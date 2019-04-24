using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string name;
    public int price;
    public float upgradeMultiplier;
    public float priceMultiplier;

    private int upgradeLevel = 0;

    internal void CalculateNextUpgradePrice()
    {
        price += (int) Math.Ceiling(price * priceMultiplier);
        upgradeLevel++;
    }
}
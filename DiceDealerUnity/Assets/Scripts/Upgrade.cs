using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string name;
    public int price;
    public float upgradeMultiplier;
    
    private int upgradeLevel = 0;

    internal void CalculateNextUpgradePrice()
    {
        price += price;
        upgradeLevel++;
    }
}
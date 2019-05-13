using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dice Combos", menuName = "Dice Combos")]
public class Combos : ScriptableObject
{
    private const int Size = 6;

    public DiceCombo[] diceCombos;

    private Dictionary<int, List<DiceCombo>> diceComboDict;

    public void InitializeDiceComboDict()
    {
        if (diceComboDict != null)
        {
            return;
        }

        diceComboDict = new Dictionary<int, List<DiceCombo>>();

        for (int i = 0; i < Size; i++)
        {
            diceComboDict.Add(i, new List<DiceCombo>());
        }

        foreach (var diceCombo in diceCombos)
        {
            for (int i = 0; i < diceCombo.diceEyes.Length; i++)
            {
                var diceEyes = diceCombo.diceEyes[i];
                if (diceEyes > 0)
                {
                    var list = diceComboDict[i];
                    list.Add(diceCombo);
                    diceComboDict[i] = list;
                }
            }
        }
    }

    public List<DiceCombo> CheckCombo(int newlyDiceEye, int[] currentDiceEyes)
    {
        var appliedCombos = new Dictionary<int, DiceCombo>();
        if (newlyDiceEye == 0)
        {
            return appliedCombos.Values.ToList();
        }

        var list = diceComboDict[newlyDiceEye - 1];
        foreach (var diceCombo in list)
        {
            if (diceCombo.CompareDiceEyes(currentDiceEyes) && !appliedCombos.ContainsKey(diceCombo.id))
            {
                appliedCombos.Add(diceCombo.id, diceCombo);
                Debug.Log("Combo with id " + diceCombo.id);
            }
        }

        return appliedCombos.Values.ToList();
    }


    public void OnValidate()
    {
        foreach (var diceCombo in diceCombos)
        {
            if (diceCombo.diceEyes.Length != Size)
            {
                Array.Resize(ref diceCombo.diceEyes, Size);
            }

            for (int i = 0; i < diceCombo.diceEyes.Length; i++)
            {
                if (diceCombo.diceEyes[i] < 0)
                {
                    diceCombo.diceEyes[i] = 0;
                }
            }
        }
    }

    [Serializable]
    public class DiceCombo
    {
        private static int IDCounter;

        public float comboPoints;
        public int[] diceEyes;


        internal readonly int id = IDCounter++;

        public bool CompareDiceEyes(int[] currentDiceEyes)
        {
            return diceEyes.Where((t, i) => t != 0 && t < currentDiceEyes[i]).Any();
        }
    }
}
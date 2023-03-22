using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI PVText;
    public Slider hpSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        ArmorText.text = "AP: " + unit.armor;
        PVText.text = "PV: " + unit.currentHP;
    }

    public void SetHP(int hp, int armor, Unit unit)
    {
        hpSlider.value = hp;
        ArmorText.text = "AP: " + unit.armor;
        PVText.text = "PV: " + unit.currentHP;
    }
}

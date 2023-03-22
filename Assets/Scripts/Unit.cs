using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public bool onFire;

    public int maxHP;
    public int currentHP;

    public int armor;
    int getturn = 0;

    public bool TakeDamage(int dmg, int capacity, int Tour, Unit playerUnit)
    {
        if (capacity == 1){
            if (dmg>armor)
                currentHP -= dmg-armor;
        }
        if (capacity == 2){
            dmg = dmg / 3;
            TakeArmorDamage(10,dmg);
        }
        if (capacity == 3){
            getturn = Tour;
            if (5>armor){
                currentHP -= 5-armor;
                onFire = true;
            }else{
                onFire = true;
            }
        }
        if (capacity == 4)
            playerUnit.Heal(5);
        
        if (getturn+5 > Tour)
            IsOnFire(onFire);

        if (currentHP <= 0)
        {
            currentHP=0;
            return true;
        }
        else 
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public bool TakeArmorDamage(int admg, int dmg)
    {
        if (armor<=admg){
            admg = admg-armor;
            armor = 0;
            currentHP -= admg+dmg;
        }else {
            armor -= admg;
            currentHP -= dmg;
        }   
        if (currentHP <= 0)
        {
            currentHP=0;
            return true;
        }
        else 
            return false;
    }

    public bool IsOnFire(bool onFire)
    {
        
        if (onFire)
            currentHP -= 3;

        if (currentHP <= 0)
        {   
            currentHP=0;
            return true;
        }
        else 
            return false;
    }
}

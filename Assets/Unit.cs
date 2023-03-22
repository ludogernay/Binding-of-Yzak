using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class turn
{
    public int getturnp;

    public void setturnp(int Tour){
        getturnp = Tour;
    }
}*/

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public bool onFire;
    public bool Paralysis;

    public int maxHP;
    public int currentHP;

    public int armor;
    public int getturnf = 0;
    public int getturnp = 0;
    public bool attack = true;
    
    
    public bool TakeDamage(int dmg, int capacity, int Tour, Unit playerUnit, Unit enemyUnit, BattleState state)
    {
        int fail = 0;

        if (state == BattleState.PLAYERTURN && playerUnit.Paralysis == true){
                if (playerUnit.getturnp+6 >= Tour){
                    fail = Random.Range(0, 100);
                    Debug.Log("Test PARALYSIE YOU: " + fail );
                    if (fail > 25){
                        playerUnit.attack = false;
                        return false;
                    }
                    else playerUnit.attack = true;
                }
                else if (playerUnit.getturnp+6 <= Tour)
                    playerUnit.Paralysis = false;
        }

        if (state == BattleState.ENEMYTURN && enemyUnit.Paralysis == true){
                if (enemyUnit.getturnp+3 >= Tour){
                    fail = Random.Range(0, 100);
                    Debug.Log("Test PARALYSIE ENEMY: " + fail );
                    if (fail > 25){
                        enemyUnit.attack = false;
                        return false;
                    }
                    else enemyUnit.attack = true;
                }
                else if (enemyUnit.getturnp+6 <= Tour)
                    enemyUnit.Paralysis = false;
        }


            
        if (capacity == 1){ //calcule les dégats de l'attaque normale
            if (dmg>armor)
                currentHP -= dmg-armor;
        }
            
        if (capacity == 2){ //calcule les dégats de l'attaque perce armure
            dmg = dmg / 3;
            TakeArmorDamage(10,dmg);
        }
            
        if (capacity == 3){ //calcule les dégats de l'attaque de feu
            if (state == BattleState.PLAYERTURN){
                Debug.Log("Fuego -> ENEMY");
                enemyUnit.getturnf = Tour;
                enemyUnit.onFire = true;
                if (5>armor){
                    enemyUnit.currentHP -= 5-armor;
                }
            }
            if (state == BattleState.ENEMYTURN){
                Debug.Log("Fuego -> YOU");
                playerUnit.getturnf = Tour;
                playerUnit.onFire = true;
                if (5>armor){
                    playerUnit.currentHP -= 5-armor;
                }
            }
        }

        if (capacity == 4) //Lance la fonction de soin
            if (state == BattleState.PLAYERTURN){
                Debug.Log("Heal YOU");
                playerUnit.Heal(5);
            }  
            else {
                Debug.Log("Heal ENEMY");
                enemyUnit.Heal(5);
            }
                    
            
        if (capacity == 5){
            if (state == BattleState.PLAYERTURN){
                enemyUnit.getturnp = Tour;
                enemyUnit.Paralysis = true;
                Debug.Log("Paralyse -> ENEMY");
            }else if (state == BattleState.ENEMYTURN){
                playerUnit.getturnp = Tour;
                playerUnit.Paralysis = true;
                Debug.Log("Paralyse -> YOU");
            }
                Paralysis = true;
                currentHP -= 3;
        }
        
        // Fin capacité

        //Vérifie si l'unit sur laquelle la fonction s'éxécute est morte
        if (currentHP <= 0)
        {
            Debug.Log("Dead");
            currentHP=0;
            return true;
        }
        else 
            return false;
    }
    
    public void Heal(int amount)//calcule les soins
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public bool TakeArmorDamage(int admg, int dmg)//calcule les dégats de l'attaque perce armure
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

    public bool IsOnFire(int Tour)//calcule les dégats de feu
    {   
        if (onFire){
            currentHP -= 3;
            if (currentHP <= 0)
            {   
                currentHP=0;
                return true;
            }
        }

        if (getturnf+5 < Tour){
            onFire = false;
        }
        return false;
    }


}


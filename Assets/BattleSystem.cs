using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, TRAITEMENT, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;
    public int Tour = 0;
    public int capacity = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Une " + enemyUnit.unitName + " sauvage approche...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        Tour = 0;
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choisissez une action :";
        Tour++;
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN){
            return;
        }
        capacity = 1;
        StartCoroutine(PlayerAttack());
        
    }

    public void OnAPAttackButton()
    {
        if (state != BattleState.PLAYERTURN){
            return;
        }
        capacity = 2;
        StartCoroutine(PlayerAttack());
        
    }

    public void OnFireAttackButton()
    {
        if (state != BattleState.PLAYERTURN){
            return;
        }
        capacity = 3;
        StartCoroutine(PlayerAttack());
        
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        capacity = 4;
        StartCoroutine(PlayerAttack()); 
    }
    public void OnParalysisButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        capacity = 5;
        StartCoroutine(PlayerAttack()); 
    }



    IEnumerator PlayerAttack()
    {
        Debug.Log("Debut Tour Player");

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage, capacity, Tour, playerUnit, enemyUnit, state);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.armor, enemyUnit);
        
        if (playerUnit.Paralysis == true){
            if (playerUnit.attack == false){
                Debug.Log("HUD Para YOU fail");
                dialogueText.text = "Vous êtes paralysée !";
            }
            else {
                Debug.Log("HUD Para YOU reussi");
                dialogueText.text = "Vous êtes paralysée mais avez réussi à attaqué !";
            }
        }
        
        state = BattleState.TRAITEMENT;

        if (capacity == 4){
            playerHUD.SetHP(playerUnit.currentHP, playerUnit.armor, playerUnit);
            dialogueText.text = "Vous vous êtes soigné.";
        } else {
            dialogueText.text = "Vous attaquez !";
        }

        bool isDeadFire = false;
        if (playerUnit.onFire == true && isDead == false) { //Mettre les ticks de l'attaque puis du feu
            yield return new WaitForSeconds(1f);
            isDeadFire = playerUnit.IsOnFire(Tour);
            playerHUD.SetHP(playerUnit.currentHP, playerUnit.armor, playerUnit);
            dialogueText.text = "Vous êtes brulé !";
        }

        yield return new WaitForSeconds(2f);
        
        Debug.Log("Fin Tour Player");
        

        if(isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }else if (isDeadFire){
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Debut Tour ENEMY");

        int RandomCapacity = Random.Range(1, 6);
        RandomCapacity = 5;
        Debug.Log("CapacityRandom ENEMY : "+RandomCapacity);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage,RandomCapacity, Tour, playerUnit, enemyUnit, state);

        playerHUD.SetHP(playerUnit.currentHP, playerUnit.armor, playerUnit);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.armor, enemyUnit);
        
        dialogueText.text = enemyUnit.unitName + " attaque !";
        
        if (RandomCapacity == 4){
            dialogueText.text = enemyUnit.unitName + " s'est soigné.";
        }

        if (enemyUnit.Paralysis == true){

            if (enemyUnit.attack == false){
                Debug.Log("HUD Para ENEMY fail");
                dialogueText.text = enemyUnit.unitName + " est paralysée !";
            }
            else {
                Debug.Log("HUD Para ENEMY reussi");
                dialogueText.text = enemyUnit.unitName + " est paralysée mais a réussi à attaqué !";
            }  
        }

        bool isDeadFire = false;
        if (enemyUnit.onFire == true && isDead == false) { //Mettre les ticks de l'attaque puis du feu
            yield return new WaitForSeconds(1f);
            isDeadFire = enemyUnit.IsOnFire(Tour);
            enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.armor, enemyUnit);
            dialogueText.text = enemyUnit.unitName + " est brulé";
        }

        yield return new WaitForSeconds(2f);

        Debug.Log("Fin Tour ENEMY");
        
        Tour++;

        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }else if (isDeadFire){
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }else{
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Vous avez gagné la bataille !";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Move");
            
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "Vous avez perdu.";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Move");
        }
    }
}

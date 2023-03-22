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
    public So so;
    
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

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage, capacity, Tour, playerUnit);

        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.armor, enemyUnit);
        if (capacity == 4){
            playerHUD.SetHP(playerUnit.currentHP, playerUnit.armor, playerUnit);
            dialogueText.text = "Vous vous êtes soigné.";
        }else{
            dialogueText.text = "L'attaque a réussi !";
        }  
        state = BattleState.TRAITEMENT;

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attaque !";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage,1, Tour, playerUnit);

        playerHUD.SetHP(playerUnit.currentHP, playerUnit.armor, playerUnit);

        yield return new WaitForSeconds(1f);
        Tour++;
        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    
    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Vous avez gagné la bataille !";
            so.win=true;
            Debug.Log(so.win);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Move");
            
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "Vous avez perdu.";
            so.win=false;
            Debug.Log(so.win);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Move");
        }
    }
}

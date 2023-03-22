using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour
{
    public void NxtSceneCombat(){
        SceneManager.LoadScene("SampleScene");
    }
}
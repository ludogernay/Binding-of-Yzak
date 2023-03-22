using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newgame : MonoBehaviour
{
    public So so;

    public void NxtSceneCombat(){
        SceneManager.LoadScene("SampleScene");
    }
    public void NxtSceneMove(){
        so.win = false;
        SceneManager.LoadScene("Move");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void NxtSceneCombat(){
        SceneManager.LoadScene("SampleScene");
    }
    public void NxtSceneMove(){
        SceneManager.LoadScene("Move");
    }
}

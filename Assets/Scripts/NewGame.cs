using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public SO1 so;
    public void NxtSceneMove(){
        so.win=false;
        SceneManager.LoadScene("Move");
    }
}

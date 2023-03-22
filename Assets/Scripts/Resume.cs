using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour
{
    public void NxtSceneMove(){
        SceneManager.LoadScene("Move");
    }
}

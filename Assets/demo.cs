using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        GameObject.Find("Transition").GetComponent<Transition>().LoadNextScene();
    }
}

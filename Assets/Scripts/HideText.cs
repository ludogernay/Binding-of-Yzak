using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideText : MonoBehaviour
{
    public GameObject Object;
    public So so;

    void Start()
    {
        Object.SetActive(false);
    }
    
    public void FixedUpdate() {
        if (so.win)
        {
            Object.SetActive(false);
        }
        else
        {
            Object.SetActive(true);
        }
    }

    public void OnCollisionEnter2D()
    {
        if (so.win)
        {
            Object.SetActive(false);
        }else{
            Object.SetActive(true);
        }
      
    }
    public void OnCollisionExit2D()
    {
        Object.SetActive(false);
    }
}

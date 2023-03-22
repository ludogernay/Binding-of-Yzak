using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sauvegarde : MonoBehaviour
{
    public GameObject CombattreUI;
    public So so;

    public void FixedUpdate() {
        if (so.win)
        {
            CombattreUI.SetActive(false);
        }
        else
        {
            CombattreUI.SetActive(true);
        }
    }
}

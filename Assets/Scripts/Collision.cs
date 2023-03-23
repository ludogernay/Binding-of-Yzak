using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    public So so;
    public GameObject Object;
    private SpriteRenderer spriteRenderer;

   
    void Start() 
    { 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void OnCollisionEnter2D()
    {
        if (so.win)
        {
            Object.SetActive(false);
        }
        else
        {
            Object.SetActive(true);
        }
    }
    public void OnCollisionExit2D()
    {
        Object.SetActive(false);
    } 
    
    public void Update() {
        if (so.win)
        {
            spriteRenderer.color = new Color (1, 0, 0, 1); 
        }
    }
}

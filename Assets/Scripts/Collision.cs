using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Object;
    public void OnCollisionEnter2D()
    {
        Object.SetActive(true);
    }
    public void OnCollisionExit2D()
    {
        Object.SetActive(false);
    }
}

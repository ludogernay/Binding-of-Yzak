using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
   Animator animator;
   public float TransitionTime = 0.5f;
   [SerializeField] int Nextscene;

   private void awake()
   {
    animator = GetComponent<Animator>();
   }

   public void LoadNextScene()
   {
    Nextscene = SceneManager.GetActiveScene().buildIndex + 1;
    StartCoroutine(LoadScene(Nextscene));
   }

   IEnumerator LoadScene(int buil_index)
   {
        animator.SetTrigger("out");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(buil_index);
   }
}

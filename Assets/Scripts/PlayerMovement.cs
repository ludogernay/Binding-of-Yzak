using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        movement.x=Input.GetAxisRaw("Horizontal");           //On récupère les input des flèches directionelles
        movement.y=Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);          //On envoie les informations de déplacement à l'animation qui gère les sprites
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {   
        if (movement.x!=0 && movement.y!=0){
            movement.x=0;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);     //Le joueur se déplace uniquement en horizontal ou vertical
                                                                                           //Si 2 directions sont utilisés, seule la direction verticale est retenue
        }else{
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}

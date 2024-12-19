using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    
    public float controlLoseTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        //perder vida
    }

    public void TakeDamage(float damage, Vector2 position)
    {
        //perder vida
        
        //animator.SetTrigger("Damage");
        
        //Perder control
        StartCoroutine(LoseControl());
        StartCoroutine(DisableCollider());
        playerMovement.Knockback(position);
    }
    
    private IEnumerator DisableCollider()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(controlLoseTimer);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
    private IEnumerator LoseControl()
    {
        playerMovement.canMove = false;
        yield return new WaitForSeconds(controlLoseTimer);
        playerMovement.canMove = true;
    }
}

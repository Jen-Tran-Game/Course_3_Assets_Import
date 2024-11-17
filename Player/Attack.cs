using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // varible to determine if the damage function can be called 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            //if can attack
                hit.Damage(); 
            // set that variable to false
        }
    }

    //coroutine to reset variable after 0.5f 

}

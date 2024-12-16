using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IDamageable
{
    private bool _canDamage = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);


        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
        }
    }

    //coroutine to reset variable after 0.5f 
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true; 
    }

}
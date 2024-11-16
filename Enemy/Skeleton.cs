using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }


    public override void Init()
    {
        base.Init();
        // assign health property to our enemy health 
    }
    public void Damage()
    {
        Debug.Log("Damage()"); 
        // subtract 1 from health 

        // if health is less than 1 
        // destroy the object 
    }
}

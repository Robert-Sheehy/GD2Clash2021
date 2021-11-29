using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript :Building, IHealth
{
    public GameObject CannonBall;

    // Start is called before the first frame update
    void Start()
    {
        Melee_distance = 3.5f;
        is_offensive_building = true;
        DPS = 100;
    }
    new
    // Update is called once per frame
    void Update()
    {
        if (current_state == Building_States.Attacking && current_target && attack_timer <= 0f && within_range(current_target))
        {

            GameObject new_CannonBall = Instantiate(CannonBall,
                  transform.position + new Vector3(2, 1.5f), Quaternion.identity);
            projectile_script NewCannonballScript = new_CannonBall.GetComponent<projectile_script>();
            NewCannonballScript.setupProjectile(transform.position,current_target.transform.position,0,10,theManager,DPS);
        }


    }





}

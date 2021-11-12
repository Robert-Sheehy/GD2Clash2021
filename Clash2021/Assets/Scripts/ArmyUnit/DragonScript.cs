using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : CharacterScript
{

    public GameObject fireball_template;

    // Start is called before the first frame update
    void Start()
    {
        DPS = 100;
        character_speed = 10;
        attack_time_interval = 5;
        min_distance_ranged = 0;
        max_distance_ranged = 10;
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
 
    }

    internal override void attack(int damage)
    {
        GameObject newFireBallGO = Instantiate(fireball_template, this.transform.position + 5 * transform.forward, Quaternion.identity);
        projectile_script newFireball = newFireBallGO.GetComponent<projectile_script>();

        newFireball.setupProjectile(newFireball.transform.position, current_target.transform.position, 0, 50, theManager, damage);
    }

}

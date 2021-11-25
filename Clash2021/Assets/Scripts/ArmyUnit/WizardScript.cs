using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizard : CharacterScript
{
    public GameObject fireball_template;

    // Start is called before the first frame update
    void Start()
    {
        _level = 0;
        DPS = 150;
        MHP = 400;
        CHP = 400;
        Melee_distance = 20f;
        character_speed = 10f;
        attack_time_interval = 0.7f;
        my_state = Character_states.Idle;
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
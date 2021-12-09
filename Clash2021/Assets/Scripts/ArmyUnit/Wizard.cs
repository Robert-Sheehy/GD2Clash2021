using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterScript

{
    bool dead = false;

    void Start()
    {
        base.Start();

        _level = 0;
        DPS = 150;
        MHP = 400;
        CHP = 400;
        Melee_distance = 20f;
        character_speed = 10f;

        attack_time_interval = 0.5f;

        current_state = Unit_States.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            takeDamage(200);
        }

        base.Update();

    }
    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
            current_state = Unit_States.Idle;
    }

    internal void levelUp()
    {
        _level++;
        MHP += 50;
        DPS += 30;
    }

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if(CHP <= 0)
        {
            current_state = Unit_States.Dead;
            theManager.Im_Dead(this);
            dead = true;
            character_animator.SetBool("is_dead", (current_state == Unit_States.Dead));
        }
    }
}

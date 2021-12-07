using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterScript
{
    void Start()
    {
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
        MHP += 40;
        DPS += 20;
    }
}

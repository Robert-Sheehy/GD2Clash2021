using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : CharacterScript
{

    void Start()
    {
        DPS = 100;
        MHP = 300;
        CHP = 300;
        _level = 0;
        attack_time_interval = 0.7f;
        Melee_distance = 12f;
        current_state = Unit_States.Idle;
        character_speed = 12f;
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
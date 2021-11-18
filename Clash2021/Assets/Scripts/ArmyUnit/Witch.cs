using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : CharacterScript
{
    new
    void Start()
    {
        DPS = 100;
        MHP = 300;
        _level = 0;
        attack_time_interval = 0.7f;
        Melee_distance = 12f;
        my_state = Character_states.Idle;
        character_speed = 12f;
        base.Start();
    }
    
    new
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    internal void levelUp()
    {
        _level++;
        MHP += 40;
        DPS += 20;
    }

    internal override void attack(int dmg)
    {
        current_target.takeDamage(dmg);
    }

}
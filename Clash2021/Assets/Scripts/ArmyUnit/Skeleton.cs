using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : CharacterScript
{
    // Start is called before the first frame update

    new
    void Start()
    {
        DPS = 25;
        MHP = 30;
        _level = 0;
        attack_time_interval = 1f;
        Melee_distance = 3f;
        my_state = Character_states.Idle;
        character_speed = 24f;
        base.Start();
    }

    // Update is called once per frame

    new
    void Update()
    {
        base.Update();
    }

    internal override void attack(int dmg)
    {
        current_target.takeDamage(dmg);
    }
}

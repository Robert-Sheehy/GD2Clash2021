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
        my_state = Character_states.Idle;
        character_speed = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (my_state)
        {
            case Character_states.Idle:

                if (current_target)
                {
                    my_state = Character_states.Move_to_Target;
                }

                else
                {
                    current_target = theManager.whats_my_target(this);
                    if (current_target != null)
                    {
                        Vector3 from_me_to_target = current_target.transform.position - transform.position;
                        velocity = character_speed * from_me_to_target.normalized;
                        transform.LookAt(current_target.transform);
                        my_state = Character_states.Move_to_Target;
                    }

                }

                break;

            case Character_states.Move_to_Target:

                if (current_target != null)
                        current_target = theManager.whats_my_target(this);

                    if (within_melee_range(current_target))
                    {
                        my_state = Character_states.Attack;
                        attack_timer = 0;
                        velocity = Vector3.zero;
                    }

                    else
                    {
                        my_state = Character_states.Idle;
                    }

                transform.position += velocity * Time.deltaTime;

                break;

            case Character_states.Attack:

                if (current_target)

                    if (attack_timer <= 0f)
                    {
                        current_target.takeDamage((int)((float)DPS * attack_time_interval));
                        attack_timer = attack_time_interval;
                    }

                    else
                        my_state = Character_states.Idle;

                attack_timer -= Time.deltaTime;

                break;

            case Character_states.Death:

                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_target = FindObjectOfType<Unit>();
            if (current_target)
            {
                assign_target(current_target);
            }
        }
    }
    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
            my_state = Character_states.Idle;
    }

    internal void levelUp()
    {
        _level++;
        MHP += 40;
        DPS += 20;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterScript
{
    Building current_target;

    void Start()
    {
        _level = 0;
        DPS = 150;
        MHP = 400;
        CHP = 400;
        Melee_distance = 20f;
        character_speed = 10f;
        attack_time_interval = 0.7f;
        current_state = Unit_States.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state)
        {

            case Unit_States.Idle:

                if (current_target) current_state = Unit_States.Move_to_Target;
                else
                {
                    //current_target = theManager.whats_my_target(this);
                    if (current_target != null)
                    {
                        Vector3 from_me_to_target = current_target.transform.position - transform.position;
                        velocity = character_speed * from_me_to_target.normalized;
                        transform.LookAt(current_target.transform);
                        current_state = Unit_States.Move_to_Target;
                    }

                }

                break;

            case Unit_States.Move_to_Target:

                if (current_target != null)
                    if (within_range(current_target))
                    {
                        current_state = Unit_States.Attacking;
                        attack_timer = 0;
                        velocity = Vector3.zero;
                    }
                    else
                    {
                        current_state = Unit_States.Idle;
                    }

                transform.position += velocity * Time.deltaTime;
                break;

            case Unit_States.Attacking:

                if (current_target)
                    if (attack_timer <= 0f)
                    {
                        current_target.takeDamage((int)((float)DPS * attack_time_interval));
                        attack_timer = attack_time_interval;
                    }

                    else
                        current_state = Unit_States.Idle;

                attack_timer -= Time.deltaTime;

                break;


            case Unit_States.Dead:


                break;

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            current_target = FindObjectOfType<Building>();
            if (current_target)
            {
                assign_target(current_target);
            }
        }
    }

    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
            current_state = Unit_States.Idle;
    }

    public override void takeDamage(int how_much_damage)
    {
        throw new NotImplementedException();
    }

    public override void repair(int v)
    {
        throw new NotImplementedException();
    }
}
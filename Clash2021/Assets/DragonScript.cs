using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : CharacterScript
{





    Vector3 velocity;
    private float character_speed = 1.5f;
    internal float Melee_Distance = 10;

    // Start is called before the first frame update
    void Start()
    {
        attack_time_interval = 5;
        transform.position = new Vector3(27, 10, -10);
    }

    // Update is called once per frame
    void Update()
    {
 
        switch (my_state)
        {

            case Character_states.Idle:

                break;

            case Character_states.Move_to_Target:


                if (within_range(current_target))
                {
                    my_state = Character_states.Attack;
                    attack_timer = 0;
                    velocity = Vector3.zero;
                }

                transform.position += velocity * Time.deltaTime;
                break;

            case Character_states.Attack:

                if (attack_timer <= 0f)
                {
                    current_target.takeDamage((int)((float)DPS * attack_time_interval));
                    attack_timer = attack_time_interval;
                }

                attack_timer -= Time.deltaTime;

                

                break;


            case Character_states.Death:


                break;



        }





        if (Input.GetKeyDown(KeyCode.L))
        {
            current_target = FindObjectOfType<Building>();
            if (current_target)
            {
                assign_target(current_target);

            }
        }

    }

    public void assign_target(Building current_target)
    {
        if ((my_state == Character_states.Idle) || (my_state == Character_states.Move_to_Target))
        {
            Vector3 from_me_to_building = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_building.normalized;
            velocity = direction * character_speed;
            my_state = Character_states.Move_to_Target;
        }
    }

    private bool within_range(Unit current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) < current_target.Melee_distance + 20.0f);
    }

    public void takeDamage(int v)
    {
        throw new NotImplementedException();
    }

    public void repair(int v)
    {
        throw new NotImplementedException();
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript:Unit
{
    enum Character_states { Idle, Move_to_Target, Attack, Death}

    Character_states my_state = Character_states.Idle;
    Renderer myRenderer;
    


    Vector3 velocity;
    private float character_speed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        DPS = 10;
    
    }

    // Update is called once per frame
    void Update()
    {
        print("Hello");
        switch (my_state)
        {

            case Character_states.Idle:
                
                if (current_target) my_state = Character_states.Move_to_Target;
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

                if (current_target!=null)
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









        
    }



    internal void ImtheMan(Manager manager)
    {
        theManager = manager;
    }

    public void assign_target(Unit current_target)
    {
        if ((my_state == Character_states.Idle)  || (my_state == Character_states.Move_to_Target))
        {
            Vector3 from_me_to_building = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_building.normalized;
            velocity = direction * character_speed;
            my_state = Character_states.Move_to_Target;
        }
    }

    private bool within_melee_range(Unit current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) < current_target.Melee_distance);
    }

    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
            my_state = Character_states.Idle;
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
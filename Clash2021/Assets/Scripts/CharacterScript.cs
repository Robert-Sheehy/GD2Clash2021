using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript:Unit
{
    internal enum Character_states { Idle, Move_to_Target, Attack, Death}

    internal Character_states my_state = Character_states.Idle;
    Renderer myRenderer;


    internal Animator character_animator;

    internal Vector3 velocity;

    internal float character_speed = 3f;

    new
    // Start is called before the first frame update
    internal void Start()
    {
        character_animator = GetComponent<Animator>();

        base.Start();
    }

    // Update is called once per frame
    internal void Update()
    {
   
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
                        from_me_to_target = new Vector3(from_me_to_target.x, 0, from_me_to_target.z);
                        velocity = character_speed * from_me_to_target.normalized;
                        transform.LookAt(current_target.transform);
                        my_state = Character_states.Move_to_Target;
                    }

                }

                break;

            case Character_states.Move_to_Target:

                if (current_target != null)
                    if (within_range(current_target))
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
                {
                    if (attack_timer <= 0f)
                    {

                        attack(DPS * (int)attack_time_interval);
                        attack_timer = attack_time_interval;
                    }
                }
                else
                    my_state = Character_states.Idle;

                attack_timer -= Time.deltaTime;

                break;


            case Character_states.Death:


                break;



        }










    }

    internal virtual void attack(int dmg)
    {
        current_target.takeDamage(dmg);
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


    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
        {
            my_state = Character_states.Idle;
            current_target = null;
        }
    }

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            print("My name is '" + this + "' and I am declaring my untimely demise");
        }
    }

 
}
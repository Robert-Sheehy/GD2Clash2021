using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript:Unit
{

 
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
        print("Here");
   
        switch (current_state)
        {

            case Unit_States.Idle:

                if (current_target)
                {
                    current_state = Unit_States.Move_to_Target;
                    character_animator.SetBool("isWalking", true);
                }
                else
                {
                    current_target = theManager.whats_my_target(this);
                    if (current_target != null)
                    {
                        Vector3 from_me_to_target = current_target.transform.position - transform.position;
                        from_me_to_target = new Vector3(from_me_to_target.x, 0, from_me_to_target.z);
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
                {
                    if (attack_timer <= 0f)
                    {

                        attack(DPS * (int)attack_time_interval);
                        attack_timer = attack_time_interval;
                    }
                }
                else
                    current_state = Unit_States.Idle;

                attack_timer -= Time.deltaTime;

                break;


            case Unit_States.Dead:
                    

                break;



        }










    }

    internal virtual void attack(int dmg)
    {
        current_target.takeDamage(dmg);
    }



    public void assign_target(Unit current_target)
    {
        if ((current_state == Unit_States.Idle)  || (current_state == Unit_States.Move_to_Target))
        {
            Vector3 from_me_to_building = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_building.normalized;
            velocity = direction * character_speed;
            current_state = Unit_States.Move_to_Target;
        }
    }


    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
        {
            current_state = Unit_States.Idle;
            current_target = null;
        }
    }

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            current_state = Unit_States.Dead;
          //  character_animator.SetBool("Dead", true);
          //  theManager.spawnMiniGols(this);
            print("My name is '" + this + "' and I am declaring my untimely demise");
        }
    }

 
}
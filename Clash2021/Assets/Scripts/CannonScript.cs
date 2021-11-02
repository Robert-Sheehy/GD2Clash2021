using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour, IHealth
{
    enum building_states { Idle, Attack, Death }
    int DPS = 100;
    float attack_time_interval = 0.5f;
    float attack_timer;
    float CannonRange = 7.0f;
    building_states my_state = building_states.Idle;

    private int MHP = 1000, CHP = 1000, _level = 0;
    CharacterScript current_target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (my_state)
        {

            case building_states.Idle:

                if ((current_target) && within_attack_range(current_target))
                {
                    my_state = building_states.Attack;
                    attack_timer = 0;
                }

                break;

            case building_states.Attack:

                if (attack_timer <= 0f)
                {
                    current_target.takeDamage((int)((float)DPS * attack_time_interval));
                    attack_timer = attack_time_interval;

                  while(within_attack_range(current_target))
                    {
                        Vector3 from_me_to_Character = current_target.transform.position - transform.position;
                        Vector3 direction = from_me_to_Character.normalized;
                        transform.forward = direction;
                    }
                }

                attack_timer -= Time.deltaTime;

                break;


            case building_states.Death:


                break;



        }





        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_target = FindObjectOfType<CharacterScript>();
            if (current_target)
            {
                assign_target(current_target);

            }
        }
    }

    public void assign_target(CharacterScript current_target)
    {
        if (my_state == building_states.Idle)
        {
            Vector3 from_me_to_Character = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_Character.normalized;
            transform.forward = direction;
        }
    }

    private bool within_attack_range(CharacterScript current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) < CannonRange);
    
    }

    public void repair(int v)
    {
        throw new System.NotImplementedException();
    }

    public void takeDamage(int v)
    {
        throw new System.NotImplementedException();
    }
}

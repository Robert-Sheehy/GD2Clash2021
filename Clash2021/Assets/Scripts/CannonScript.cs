using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript :Building, IHealth
{
    enum building_states { Idle, Attack, Death }
    int DPS = 100;
    float attack_time_interval = 0.5f;
    float attack_timer;
    float CannonRange = 20.0f;
    building_states my_state = building_states.Idle;
    public GameObject CannonBall;
    private int MHP = 1000, CHP = 1000, _level = 0;
    CharacterScript current_target;

    // Start is called before the first frame update
    void Start()
    {
        Melee_distance = 3.5f;
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

                else

                    current_target = theManager.whats_my_Unit(this);
                    break;

            case building_states.Attack:

                if (attack_timer <= 0f)
                {
                    if (within_attack_range(current_target))
                    {
                        Vector3 from_me_to_Character = current_target.transform.position - transform.position;
                        Vector3 direction = from_me_to_Character.normalized;
                        transform.forward = direction;

                        GameObject new_CannonBall = Instantiate(CannonBall,
                       transform.position + new Vector3(2, 1.5f), Quaternion.identity);
                        projectile_script NewCannonballScript = new_CannonBall.GetComponent<projectile_script>();
                        NewCannonballScript.setup_projectile(this, current_target, 0, 50);


                        current_target.takeDamage((int)((float)DPS * attack_time_interval));
                        attack_timer = attack_time_interval;

                    }

                    else
                        my_state = building_states.Idle;
                }

                attack_timer -= Time.deltaTime;

                break;


            case building_states.Death:


                break;



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

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            theManager.Im_Dead(this);
            Destroy(gameObject);
        }
    }
}

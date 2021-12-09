using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarScript : Building
{
    enum building_states { Idle, Attack, Death }

    float MortarMaxRange = 15.0f;
    float MortarMinRange = 7.0f;
    building_states my_state = building_states.Idle;

    public GameObject CannonBall_template;
    Transform cannon_tf;
    // Start is called before the first frame update
    void Start()
    {   
        Transform[] all_children =  gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform g in all_children)
        {
            if (g.gameObject.name == "cannon")
                cannon_tf = g;

        }
        myRenderer = GetComponentInChildren<Renderer>();
        is_offensive_building = true;
      //  base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //switch (my_state)
        //{

        //    case building_states.Idle:

        //        if ((current_target) && within_attack_range(current_target))
        //        {
        //            my_state = building_states.Attack;
        //            attack_timer = 0;
        //        }
        //        //else
        //          //  current_target = th

        //        break;

        //    case building_states.Attack:

        //        if (attack_timer <= 0f)
        //        {
        //            current_target.takeDamage((int)((float)DPS * attack_time_interval));
        //            attack_timer = attack_time_interval;

        //            while (within_attack_range(current_target))
        //            {
        //                Vector3 from_me_to_Character = current_target.transform.position - transform.position;
        //                Vector3 direction = from_me_to_Character.normalized;
        //                transform.forward = direction;
        //            }
        //        }

        //        attack_timer -= Time.deltaTime;

        //        break;


        //    case building_states.Death:


        //        break;



        //}

        cannon_tf.Rotate(cannon_tf.right, 2f);
        base.Update();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_target = FindObjectOfType<Unit>();
            if (current_target)
            {
                assign_target(current_target);

            }
        }
    }

    public void assign_target(Unit current_target)
    {
        if (my_state == building_states.Idle)
        {
            Vector3 from_me_to_Character = current_target.transform.position - transform.position;
            Vector3 direction = from_me_to_Character.normalized;
            transform.forward = direction;
        }
    }

    private bool within_attack_range(Unit current_target)
    {
        return ((Vector3.Distance(transform.position, current_target.transform.position) <= MortarMaxRange) && (Vector3.Distance(transform.position, current_target.transform.position) >= MortarMinRange));

    }

    public void Repair(int v)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int v)
    {
        throw new System.NotImplementedException();
    }

    internal override void attack(int dmg)
    {


        GameObject new_CannonBall = Instantiate(CannonBall_template,
              transform.position + new Vector3(0, 1.5f), Quaternion.identity);
        projectile_script NewCannonballScript = new_CannonBall.GetComponent<projectile_script>();

        NewCannonballScript.setupProjectile(transform.position, current_target.transform.position, 10, 10, theManager, DPS);

        current_target.takeDamage(dmg);
        print("Mortar attack");
    }

}

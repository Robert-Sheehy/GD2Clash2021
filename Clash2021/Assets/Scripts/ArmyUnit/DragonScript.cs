using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : CharacterScript
{
   

    float animation_timer = 0, fire_on_start = 0.75f, fire_on_end = 2.5f, total_animation = 3f;

    bool isAttacking = false;
    GameObject fireGO;
    Transform head;
    public GameObject fireball_template;
    new
    // Start is called before the first frame update
    void Start()
    {
        MHP = 500;
        DPS = 100;
        character_speed = 50;
        attack_time_interval = 5;
        min_distance_ranged = 0;
        max_distance_ranged = 15;
        head = find_head();
        fireGO = Instantiate(fireball_template, head);
        fireGO.SetActive(false);
        base.Start();
    }

    private Transform find_head()
    {
        foreach (Transform bone in GetComponentsInChildren<Transform>())
            if (bone.name == "Head")
                return bone;

        return null;
    }

    new
        // Update is called once per frame
        void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {

            takeDamage(45);
        }
        if (isAttacking)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer > 3f) animation_timer = 0f;
            fireGO.SetActive((animation_timer > fire_on_start) && (animation_timer < fire_on_end));

        }

        if (current_state != Unit_States.Attacking)
        {
            if (isAttacking)
            {
                isAttacking = false;
                fireGO.SetActive(false);
                character_animator.SetBool("isAttacking", false);
            }
        }

       if (current_state == Unit_States.Dead)
        {

        }
        base.Update();
 
    }

    internal override void attack(int damage)
    {
        theManager.AOE_Attack(current_target.transform.position, 10, (int) (DPS*attack_time_interval), true);

        if (isAttacking)
        {

        }
        else
        {
            isAttacking = true;
            animation_timer = 0;
            character_animator.SetBool("isAttacking", (current_state == Unit_States.Attacking));
        }

        
    }

}

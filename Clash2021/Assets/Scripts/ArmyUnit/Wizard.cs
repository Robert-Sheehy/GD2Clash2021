using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : CharacterScript

{
    float animation_timer = 0, arcane_on_start = 0f, arcane_on_end = 0.80f;
    bool dead = false;
    bool attacking = false;
    Transform Rhand;

    GameObject arcaneGO;
    public GameObject arcane_template;

    new
    void Start()
    {
        base.Start();

        _level = 0;
        DPS = 150;
        MHP = 400;
        CHP = 400;
        Melee_distance = 20f;
        character_speed = 10f;

        Rhand = find_hand();
        arcaneGO = Instantiate(arcane_template, Rhand);
        arcaneGO.SetActive(false);

        attack_time_interval = 0.5f;

        current_state = Unit_States.Idle;

    }
    private Transform find_hand()

    {
        foreach (Transform bone in GetComponentInChildren<Transform>())
            if (bone.name == "mixamorig:RightHand")
            {
                return bone;
            }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            takeDamage(200);
        }
        if (attacking)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer > 3f) animation_timer = 0f;
            arcaneGO.SetActive((animation_timer > arcane_on_start) && (animation_timer < arcane_on_end));

        }

        if (current_state != Unit_States.Attacking)
        {
            if (attacking)
            {
                attacking = false;
                arcaneGO.SetActive(false);
                character_animator.SetBool("isAttacking", false);
            }
        }

        if (current_state == Unit_States.Dead)
        {

        }
        base.Update();

    }
    internal override void is_destroyed(Unit killed_unit)
    {
        if (current_target == killed_unit)
            current_state = Unit_States.Idle;
    }

    internal void levelUp()
    {
        _level++;
        MHP += 50;
        DPS += 30;
    }

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if(CHP <= 0)
        {
            current_state = Unit_States.Dead;
            theManager.Im_Dead(this);
            dead = true;
            character_animator.SetBool("isDead", (current_state == Unit_States.Dead));
        }
    }
    internal override void attack(int damage)
    {
        if (attacking)
        {

        }
        else
        {
            attacking = true;
            animation_timer = 0;
            character_animator.SetBool("isAttacking", (current_state == Unit_States.Attacking));
        }


    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : CharacterScript
{
    float animation_timer = 0, magic_on_start = 0f, magic_on_end = 0.75f;
    float respawn_timer = 0;

    
    Transform wand;
    GameObject magicGO;
    public GameObject magic_template;


    new
    void Start()
    {
        DPS = /*100*/ 142;
        MHP = 300;
        _level = 0;
        attack_time_interval = /*0.7*/ 1f;  //Character would not attack when float was set to 0.7f. why?
        Melee_distance = 12f;
        current_state = Unit_States.Idle;
        character_speed = 12f;
        wand = find_wand();
        magicGO = Instantiate(magic_template, wand);
        magicGO.SetActive(false);
        base.Start();
    }

    private Transform find_wand()
    {

        foreach (Transform bone in GetComponentsInChildren<Transform>())
            if (bone.name == "Wand")
                return bone;

        return null;
    }

    new
    // Update is called once per frame
    void Update()
    {

        respawn_timer += Time.deltaTime;
        if (respawn_timer > 7f)
        {
            respawn_timer = 0f;

            theManager.spawn_skeletons(this);
        }


        if (current_state == Unit_States.Attacking)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer > 3f) animation_timer = 0f;
            magicGO.SetActive((animation_timer > magic_on_start) && (animation_timer < magic_on_end));
        }

        base.Update();
    }

    internal override void attack(int dmg)
    {
        current_target.takeDamage(dmg);
    }

}
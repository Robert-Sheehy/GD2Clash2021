using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : CharacterScript
{
    // Start is called before the first frame update

    float animation_timer = 0;

    bool Attacking = false;

    Transform Hips;

    GameObject smoke;

    public GameObject smoke_template;

    float smoke_on_start = 0.75f, smoke_on_end = 2.5f;

    new
    void Start()
    {
        DPS = 120;
        MHP = 800;
        character_speed = 5;
        attack_time_interval = 2.3f;
        attack_timer = 0f;
        Hips = find_Hips();
        smoke = Instantiate(smoke_template, Hips);
        smoke.SetActive(false);
        base.Start();
    }


    private Transform find_Hips()
    {
        foreach (Transform bone in GetComponentsInChildren<Transform>())
            if (bone.name == "Hips")
                return bone;

        return null;
    }

    // Update is called once per frame

    new
    void Update()
    {
        if (Attacking)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer > 3f) animation_timer = 0f;
            smoke.SetActive((animation_timer > smoke_on_start) && (animation_timer < smoke_on_end));

        }

        if (my_state != Character_states.Attack)
        {
                Attacking = false;
                smoke.SetActive(false);
                character_animator.SetBool("Attacking", false);
        }

        base.Update();

    }

    internal override void attack(int damage)
    {
        

        if (Attacking)
        {

        }
        else
        {
            Attacking = true;
            animation_timer = 0;
            character_animator.SetBool("Attacking", (my_state == Character_states.Attack));
        }


    }

}

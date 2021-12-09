using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : CharacterScript
{
    // Start is called before the first frame update

    float animation_timer = 0;

    bool Attacking = false;

    bool Dying = false;

    Transform Hips;

    GameObject smoke;

    public GameObject smoke_template;

    float smoke_on_start = 0.75f, smoke_on_end = 2.5f;

    internal bool isMiniGolem = false;

    internal int numberOfMiniGols = 3;

    internal float gRadius = 5f;
   

    new
    void Start()
    {
        if(isMiniGolem) {
            DPS = 80;
            MHP = 500;
        }
        else
        {
            DPS = 120;
            MHP = 800;
        }

        dying_timer = 5f;
        character_speed = 5;
        attack_time_interval = 2.3f;
        attack_timer = 0f;
        Melee_distance = 3f;
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
        
        }

        if (current_state != Unit_States.Attacking)
        {
            if (Attacking)
            {
                Attacking = false;          
                character_animator.SetBool("Attacking", false);
            }
        }


        if (Dying)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer > 3f) animation_timer = 0f;
            smoke.SetActive((animation_timer > smoke_on_start) && (animation_timer < smoke_on_end));
           
        }

        if (current_state != Unit_States.Dead)
        {
            if (Dying)
            {
                Dying = false;
                smoke.SetActive(false);
                character_animator.SetBool("Dying", false);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            takeDamage(200);
        }

        if (current_state == Unit_States.Dead)
        {
            dying_time -= Time.deltaTime;

            if (dying_time <= 0)
            {
                Destroy(gameObject, 3);
            }


        }

        base.Update();

        
    }

    internal override void attack(int damage)
    {
        current_target.takeDamage(damage);

        if (Attacking)
        {

        }
        else
        {
            Attacking = true;
            animation_timer = 0;
            character_animator.SetBool("Attacking", (current_state == Unit_States.Attacking));
        }


    }

    public override void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            current_state = Unit_States.Dead;
            theManager.Im_Dead(this);
            Dying = true;
            animation_timer = 0;
            character_animator.SetBool("Dying", (current_state == Unit_States.Dead));
            
        }
    }

}

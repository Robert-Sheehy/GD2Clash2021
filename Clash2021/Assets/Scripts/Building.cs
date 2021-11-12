using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building  : Unit


{
    

    internal enum Building_States { Idle, Attacking, Under_Construction, Dying, Dead }
    internal Building_States current_state = Building_States.Idle;
    Renderer myRenderer;
    
    public int Level
    {
        get { return _level + 1; }
        set {
            if (current_active_model) current_active_model.SetActive(false);
            _level = value - 1;
            current_active_model = all_levels[_level];
            current_active_model.SetActive(true);
            myRenderer = current_active_model.GetComponent<Renderer>();
        }
    }




    List<GameObject> all_levels;
    GameObject current_active_model;


    internal float attack_distance;
    private bool is_offensive_building;


    // Start is called before the first frame update
    void Start()


    {
        Melee_distance = 5f;
        dying_time = 2;


        all_levels = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
            all_levels.Add(child);

        }

        Level = 1;


    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state)
        {

            case Building_States.Idle:

                if (is_offensive_building)
                    if (current_target)
                        current_state = Building_States.Attacking;
                    else
                        current_target = theManager.whats_my_target(this);

                break;

            case Building_States.Attacking:

                if (current_target)
                {
                    if (attack_timer <= 0f)
                    {
                        print("Attack");
                        attack(DPS * (int)attack_time_interval);
                        attack_timer = attack_time_interval;
                    }
                }
                else
                    current_state = Building_States.Idle;

                attack_timer -= Time.deltaTime;

                break;

            case Building_States.Dying:
                dying_timer -= Time.deltaTime;
                if (dying_timer < 0)
                    current_state = Building_States.Dead;



                break;

            case Building_States.Dead:

                theManager.Im_Dead(this);
                Destroy(gameObject);
                break;

        }

    }

    internal virtual void attack(int dmg)
    {
        current_target.takeDamage(dmg);
    }

    internal void levelUp()
    {

        Level++;

    }



    internal override void is_destroyed(Unit killed_unit)
    {
        throw new NotImplementedException();
    }

    public override void takeDamage(int how_much_damage)
    {
        myRenderer.material.color = Color.blue;
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
       
            myRenderer.material.color = Color.red;
            current_state = Building_States.Dying;
            dying_timer = dying_time;
            theManager.Im_Dying(this);
        }
    }

    public override void repair(int how_much_heal)
    {
        CHP += how_much_heal;
        if (CHP > MHP)
        {
            CHP = MHP;
            myRenderer.material.color = Color.white;
        }
    }
}


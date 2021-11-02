using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarScript : MonoBehaviour, IHealth
{
    enum Character_states { Idle, Attack, Death }
    int DPS = 10;
    float attack_time_interval = 1.0f;
    float attack_timer;
    Character_states my_state = Character_states.Idle;

    Renderer myRenderer;
    private int MHP = 1000, CHP = 1000, _level = 0;
    CharacterScript current_target;
    Vector3 velocity;
    public int Level
{
get { return _level + 1; }
    set
        {
        if (current_active_model) current_active_model.SetActive(false);
        _level = value - 1;
        current_active_model = all_levels[_level];
        current_active_model.SetActive(true);
        myRenderer = current_active_model.GetComponent<Renderer>();
        }
}

    public float Melee_distance { get { return 3.0f; } }
    float minAttackDistance = 8.0f;
    float maxAttackDistance = 15.0f;
    private bool destroyed = false;

    List<GameObject> all_levels;
GameObject current_active_model;

    // Start is called before the first frame update
    void Start()
    {
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
        switch (my_state)
        {

            case Character_states.Idle:
                if (within_range(current_target))
                {
                    my_state = Character_states.Attack;
                    attack_timer = 0;
                    velocity = Vector3.zero;
                }

                transform.position += velocity * Time.deltaTime;
                break;

            case Character_states.Attack:

                if (attack_timer <= 0f)
                {
                    current_target.takeDamage((int)((float)DPS * attack_time_interval));
                    attack_timer = attack_time_interval;
                }

                attack_timer -= Time.deltaTime;

                break;


            case Character_states.Death:


                break;
        }
    }

    private bool within_range(CharacterScript current_target)
    {
        throw new NotImplementedException();
    }

    public void takeDamage(int how_much_damage)
    {
        myRenderer.material.color = Color.blue;
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            destroyed = true;
            myRenderer.material.color = Color.red;
        }
    }
    internal void levelUp()
    {

        Level++;

    }

    public void repair(int how_much_heal)
    {
        CHP += how_much_heal;
        if (CHP > MHP)
        {
            CHP = MHP;
            myRenderer.material.color = Color.white;
        }
    }
    private bool within_range(Building current_target)
    {
        return (Vector3.Distance(transform.position, current_target.transform.position) < current_target.Melee_distance);
    }
}

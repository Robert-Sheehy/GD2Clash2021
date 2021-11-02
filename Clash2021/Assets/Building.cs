using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building  : Unit


{

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


    private bool destroyed = false;

    List<GameObject> all_levels;
    GameObject current_active_model;


    internal float attack_distance;


    // Start is called before the first frame update
    void Start()


    {
        Melee_distance = 5f;
        
        
        all_levels = new List<GameObject>();
        for (int i = 0; i<transform.childCount;i++)
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
        
    }







    internal void ImtheMan(Manager manager)
    {
        theManager = manager;
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
            destroyed = true;
            myRenderer.material.color = Color.red;
            theManager.Im_Dead(this);
            Destroy(gameObject);
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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Building
{

    float timer;
    private float freq = 5;

    new
    // Start is called before the first frame update
    void Start()
    {
        MHP = 10000;
      
        base.Start();
        
    }
    new
    // Update is called once per frame
    void Update()
    {   if (current_state == Building_States.Dying)
            apply_dying_animation();
        base.Update();
    }

    private void apply_dying_animation()
    {
        timer += Time.deltaTime;
        myRenderer.material.color =
              new Color(Mathf.Cos(timer * freq), Mathf.Cos(timer * freq), Mathf.Cos(timer * freq), Mathf.Cos(timer*freq));
    }
}

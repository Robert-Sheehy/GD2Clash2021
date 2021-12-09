using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class play_info_script : MonoBehaviour
{

    Unit unit_health;
    TextMeshPro text;

    int last_chp;
    private float opacity = 0;
    float time_to_fade = 3;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
            unit_health.CHP -= 50;

        if(Input.GetKeyDown(KeyCode.Q))
            unit_health.CHP += 40;
        

        if (unit_health.CHP != last_chp)
        {
            opacity = 1;
            text.SetText(unit_health.CHP.ToString());
            last_chp = unit_health.CHP;

            
           
        }
        if(unit_health.CHP <= unit_health.MHP / 1)
        {
            text.color = new Color(0, 255, 0, 1);
        }
        if (unit_health.CHP <= unit_health.MHP / 2)
        {

            text.color = new Color(255,165,0,1);
        }

        if (unit_health.CHP <= unit_health.MHP / 4)
        {
            
            text.color = new Color(255, 0, 0, 1);
        }


        opacity -= Time.deltaTime/time_to_fade;
        opacity = Mathf.Clamp(opacity, 0, 1);
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
    }

    internal void your_owner_is(Unit new_unit)
    {
        unit_health = new_unit;
        last_chp = unit_health.CHP;

    }
}

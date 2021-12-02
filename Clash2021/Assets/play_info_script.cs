using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_info_script : MonoBehaviour
{

    Unit unit_health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(unit_health.CHP);
        if (Input.GetKeyDown(KeyCode.R))
            unit_health.CHP -= 10;

        
    }

    internal void your_owner_is(Unit new_unit)
    {
        unit_health = new_unit;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAoEScript : MonoBehaviour
{
    Manager theManager;
    // Start is called before the first frame update
    void Start()
    {
        theManager = FindObjectOfType<Manager>();


        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
            for (float x = -20; x < 21f; x += 5)
                for (float z = -20; z< 50; z+=7)
                theManager.AddTHAt(new Vector3(x, 0, z));

        if (Input.GetKeyDown(KeyCode.Z))
            for (float a = 10; a < 21f; a += 5)
                for (float b = -20; b < 50; b += 7)
                    theManager.AddChar(new Vector3(a, 0, b));



        if (Input.GetKeyDown(KeyCode.Return))
            theManager.AOE_Attack(new Vector3(0, 0, 0), 15, 50,false);
    }
}

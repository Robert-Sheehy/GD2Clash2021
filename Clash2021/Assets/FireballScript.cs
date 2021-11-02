using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour

{
    Vector3 start, end, velocity, acceleration;
    float gravity =9.8f, speed = 10;
    Renderer fireball_Rednerer;
     



    // Start is called before the first frame update
    void Start()
    {
        

        transform.position = new Vector3(27, 10, -10);
        start = new Vector3(27, 10, -10);
        end = new Vector3(27, 0, 27);
        gravity = 0;
        Vector3 from_start_to_end = end - start;
        velocity = speed * from_start_to_end.normalized;
        float Total_time = from_start_to_end.magnitude / speed;
        float initial_upVelocity = 0.5f * gravity * Total_time;

        velocity += initial_upVelocity * Vector3.up;

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position == new Vector3(27, 0, 20))
        {
            GetComponent<Renderer>().enabled = false;
        }

        acceleration = gravity * Vector3.down;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

    }

}

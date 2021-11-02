using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
    Vector3 start, end, velocity,acceleration;
    float gravity = 9.8f,  speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(0, 0, 0);
        end = new Vector3(40, 0, 30);
        gravity = 100;
        Vector3 from_start_to_end = end - start;
        velocity = speed * from_start_to_end.normalized;
        float Total_time = from_start_to_end.magnitude / speed;
        float initial_upVelocity = 0.5f * gravity * Total_time;

        velocity += initial_upVelocity * Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = gravity * Vector3.down;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
    Vector3 start, end, velocity,acceleration;
    float gravity = 9.8f;


    // Start is called before the first frame update
    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        acceleration = gravity * Vector3.down;
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    internal void setup_projectile(Unit source, Unit destination,float gravity, float speed)
    {
        start = source.transform.position;
        end = destination.transform.position;
        this.gravity = gravity;
        Vector3 from_start_to_end = end - start;
        velocity = speed * from_start_to_end.normalized;
        float Total_time = from_start_to_end.magnitude / speed;
        float initial_upVelocity = 0.5f * gravity * Total_time;

        velocity += initial_upVelocity * Vector3.up;
    }


}

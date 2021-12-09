using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
    Vector3 start, end, velocity, acceleration;
    float gravity = 9.8f, speed = 10;
    private float timer;
    private Manager theManager;
    private int damage;

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
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            theManager.AOE_Attack(end, 3, damage, true);
            Destroy(gameObject);
        }
    }

    internal void setupProjectile(Vector3 start_position, Vector3 end_position, float projectile_gravity, float speed, Manager manager, int damage_to_do)
    {
        start = start_position;
        end = end_position;
        gravity = projectile_gravity;
        Vector3 from_start_to_end = end - start;
        velocity = speed * from_start_to_end.normalized;
        float Total_time = from_start_to_end.magnitude / speed;
        float initial_upVelocity = 0.5f * gravity * Total_time;
        velocity += initial_upVelocity * Vector3.up;
        timer = Total_time;
        theManager = manager;
        damage = damage_to_do;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IHealth
{
    internal Manager theManager;
    internal float Melee_distance;
    internal int DPS;
    internal float attack_time_interval = 0.5f;
    internal float attack_timer;
    internal float dying_timer;
    internal float dying_time;

    internal float max_distance_ranged = 0, min_distance_ranged = 0;

    internal Unit current_target;
    internal int MHP = 1000, CHP = 1000, _level = 0;
    // Start is called before the first frame update
    internal void Start()
    {
        CHP = MHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void ImtheMan(Manager manager)
    {
        theManager = manager;
    }
    public virtual void takeDamage(int how_much_damage)
    {
        CHP -= how_much_damage;
        if (CHP <= 0) theManager.Im_Dead(this);
    }



    public virtual void repair(int health_increase)
    {
        CHP += health_increase;
        if (CHP > MHP)
            CHP = MHP;
    }

    internal virtual bool within_range(Unit current_target)
    {

        return (Vector3.Distance(transform.position, current_target.transform.position) > min_distance_ranged) &&
            (Vector3.Distance(transform.position, current_target.transform.position) < current_target.Melee_distance + max_distance_ranged);
    }
    internal abstract void is_destroyed(Unit killed_unit);

}


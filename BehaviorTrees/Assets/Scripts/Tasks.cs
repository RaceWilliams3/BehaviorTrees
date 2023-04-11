using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Task
{
    public abstract bool run();
}

public class IsTrue_Check : Task
{
    bool varToTest;

    public IsTrue_Check(bool var)
    {
        varToTest = var;
    }

    public override bool run()
    {
        return varToTest;
    }
}

public class IsFalse_Check : Task
{
    bool varToTest;

    public IsFalse_Check(bool var)
    {
        varToTest = var;
    }

    public override bool run()
    {
        return !varToTest;
    }
}

public class OpenDoor_Do : Task
{
    Door door;

    public OpenDoor_Do(Door doorToOpen)
    {
        door = doorToOpen;
    }

    public override bool run()
    {
        door.Open();
        return true;
    }
}

public class MoveArriver_Do : Task
{
    Arriver mover;
    GameObject target;

    public MoveArriver_Do(Arriver m, GameObject t)
    {
        mover = m;
        target = t;
    }

    public override bool run()
    {
        mover.myTarget = target;
        mover.beginMovement();

        return true;
    }
}

public class BargeDoor_Do : Task
{
    Rigidbody doorRig;

    public BargeDoor_Do(Rigidbody door)
    {
        doorRig = door;
    }
    public override bool run()
    {
        doorRig.AddForce(-50f, 0, 10f, ForceMode.VelocityChange);
        return true;
    }

}

public class Sequence : Task
{
    List<Task> children;
    bool returnVal = true;

    public Sequence(List<Task> taskList)
    {
        children = taskList;
        //Debug.Log("Sequence created");
    }

    public override bool run()
    {

        foreach (Task t in children)
        {
            //Debug.Log("Task running in sequence");
            if (t.run() == false)
            {
                returnVal = false;
                break;
            }
        }
        return returnVal;
    }
}

public class Selector : Task
{
    List<Task> children;
    bool returnVal = false;

    public Selector(List<Task> taskList)
    {
        children = taskList;
        //Debug.Log("Selector Created");
    }

    public override bool run()
    {
        foreach (Task t in children)
        {
            //Debug.Log("Task running in selector");
            if (t.run() == true)
            {
                returnVal = true;
                break;
            }
        }
        return returnVal;
    }
}

public class MoveAgent_Do : Task
{
    GameObject target;
    NavMeshAgent agent;
    float offSet;

    public MoveAgent_Do(GameObject t, NavMeshAgent a, float o)
    {
        target = t;
        agent = a;
        offSet = o;
    }

    public override bool run()
    {
        agent.SetDestination(target.transform.position + new Vector3(Random.Range(-offSet,offSet),0, Random.Range(-offSet, offSet)));
        return true;
    }
}

public class IntBelowMin_Check : Task
{
    int data;
    int min;

    public IntBelowMin_Check(int d, int m)
    {
        data = d;
        min = m;
    }

    public override bool run()
    {
        /*Debug.Log("Data: " + data);
        Debug.Log("Min: " + min);*/
        if (min > data)
        {
            return true;
        }
        return false;
    }
}

public class IntAboveMin_Check : Task
{
    int data;
    int min;

    public IntAboveMin_Check(int d, int m)
    {
        data = d;
        min = m;
    }

    public override bool run()
    {
        /*Debug.Log("Data: " + data);
        Debug.Log("Min: " + min);*/
        if (data > min)
        {
            return true;
        }
        return false;
    }
}

public class FloatBelowMin_Check : Task
{
    float data;
    float min;

    public FloatBelowMin_Check(float d, float m)
    {
        data = d;
        min = m;
    }

    public override bool run()
    {
        if (min > data)
        {
            return true;
        }
        return false;
    }
}

public class Shoot_Do : Task
{
    TankAI tank;

    public Shoot_Do(TankAI t)
    {
        tank = t;
    }

    public override bool run()
    {
        tank.shoot();
        return true;
    }
}

public class MoveTo_Ammo : Task
{
    GameObject tank;
    NavMeshAgent agent;

    float closest = 1000;
    GameObject closestAmmo;

    public MoveTo_Ammo(GameObject t)
    {
        tank = t;
        agent = t.GetComponent<NavMeshAgent>();
    }

    public override bool run()
    {
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Ammo"))
        {
            float dist = Vector3.Distance(a.transform.position, tank.transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestAmmo = a;
            }
        }
        if (closestAmmo != null)
        {
            agent.SetDestination(closestAmmo.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Sequence created");
    }

    public override bool run()
    {

        foreach (Task t in children)
        {
            Debug.Log("Task running in sequence");
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
        Debug.Log("Selector Created");
    }

    public override bool run()
    {
        foreach (Task t in children)
        {
            Debug.Log("Task running in selector");
            if (t.run() == true)
            {
                returnVal = true;
                break;
            }
        }
        return returnVal;
    }
}
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
        mover.move = true;

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
        doorRig.AddForce(-10f, 0, 0, ForceMode.VelocityChange);
        return true;
    }

}

public class Sequence : Task
{
    List<Task> children;
    Task currentTask;
    int currentTaskIndex = 0;

    public Sequence(List<Task> taskList)
    {
        children = taskList;
    }

    public override bool run()
    {
        currentTask = children[currentTaskIndex];
        if (currentTask.run() == true)
        {
            currentTaskIndex++;
            if (currentTaskIndex < children.Count)
            {
                //There are more tasks to run so run again
                this.run();
            } 
            else
            {
                //All tasks have run successfully
                return true;
            }
        }
        //The last task returned false so the sequence defaults to false
        return false;
    }
}

public class Selector : Task
{
    List<Task> children;
    Task currentTask;

    public Selector(List<Task> taskList)
    {
        children = taskList;
    }

    public override bool run()
    {
        foreach (Task task in children)
        {
            if (task.run() == true)
            {
                //The task we tried worked so we can return
                return true;
            }
            //If it was false, foreach will move on and try the next one
        }

        //If not any of them work then we leave the forach so we default to false
        return false;
    }
}
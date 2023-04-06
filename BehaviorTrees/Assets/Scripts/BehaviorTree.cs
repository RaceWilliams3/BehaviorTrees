using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    public GameObject cheese;
    public GameObject door;
    public Arriver movementSys;

    //Selectors
    private Selector getCheese;
    private Selector openClosedDoor;

    //Sequences
    private Sequence goThroughOpenDoor;
    private Sequence goThroughClosedDoor;
    private Sequence openUnlockedDoor;
    private Sequence bargeLockedDoor;

    private List<Task> newTasks = new List<Task>();

    //Master task
    private Task myCurrentTask;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            myCurrentTask = BuildTask_GetCheese();
            myCurrentTask.run();
        }
    }

    private Task BuildTask_GetCheese()
    {


        //Creating task list for opening an unlocked door
        //newTasks = new List<Task>();
        newTasks.Add(new IsTrue_Check(door.GetComponent<Door>().open));
        newTasks.Add(new OpenDoor_Do(door.GetComponent<Door>()));

        openUnlockedDoor = new Sequence(newTasks);


        //Creating task list for barging a locked door
        //List<Task> newTasks = new List<Task>();
        newTasks.Clear();
        newTasks.Add(new IsTrue_Check(door.GetComponent<Door>().locked));
        newTasks.Add(new BargeDoor_Do(door.GetComponent<Rigidbody>()));

        bargeLockedDoor = new Sequence(newTasks);


        //Adding the sequences to the open closed door selector
        //newTasks = new List<Task>();
        newTasks.Clear();
        newTasks.Add(openUnlockedDoor);
        newTasks.Add(bargeLockedDoor);

        openClosedDoor = new Selector(newTasks);

        //Creating and adding the tasks and selector for the go through closed door sequence
        //newTasks = new List<Task>();
        newTasks.Clear();
        newTasks.Add(new MoveArriver_Do(movementSys, door));
        newTasks.Add(openClosedDoor);
        newTasks.Add(new MoveArriver_Do(movementSys, cheese));

        goThroughClosedDoor = new Sequence(newTasks);

        //Creating and adding the tasks to the sequence for going through an open door
        //newTasks = new List<Task>();
        newTasks.Clear();
        newTasks.Add(new IsTrue_Check(door.GetComponent<Door>().open));
        newTasks.Add(new MoveArriver_Do(movementSys, cheese));

        goThroughOpenDoor = new Sequence(newTasks);

        //Adding the the open and closed door sequences to the base selector
        //newTasks = new List<Task>();
        newTasks.Clear();
        newTasks.Add(goThroughOpenDoor);
        newTasks.Add(goThroughClosedDoor);

        getCheese = new Selector(newTasks);

        return getCheese;
    }
}

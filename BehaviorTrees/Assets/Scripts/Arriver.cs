using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arriver : Kinematic
{
    Arrive myMoveType;
    public bool move = false;
    public bool arrived = false;
    public float arrivalThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Arrive();
        myMoveType.character = this;
        myMoveType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (move)
        {
            steeringUpdate = new SteeringOutput();
            steeringUpdate.linear = myMoveType.getSteering().linear;

            base.Update();

            arrived = Vector3.Distance(this.transform.position, myTarget.transform.position) < arrivalThreshold;
        }
        
    }
}

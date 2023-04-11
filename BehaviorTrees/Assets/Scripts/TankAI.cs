using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    public GameObject bullet;

    public int ammo = 2;
    public float range = 5f;

    public float decisionRate = 2f;
    private float lastDecision = 0f;

    //All Sequences and Selectors for behavior tree
    private Sequence getAmmo;
    private Sequence shootAtPlayer;

    private Selector getAmmoOrInRange;
    private Task attackPlayer;

    private List<Task> newTasks;

    public void shoot()
    {
        ammo--;
        FiringSolution fs = new FiringSolution();
        Vector3? aimVector = fs.Calculate(transform.position, player.transform.position, range * 2, Physics.gravity);
        if (aimVector.HasValue)
        {
            GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(0, 1, 0), transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(aimVector.Value.normalized * range * 2, ForceMode.VelocityChange);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            ammo++;
            collision.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.time - decisionRate > lastDecision)
        {
            attackPlayer = BuildAttackTree();
            attackPlayer.run();
            lastDecision = Time.time;
        }
        
    }


    Task BuildAttackTree()
    {
        newTasks = new List<Task>();
        newTasks.Add(new IntBelowMin_Check(ammo, 1));
        newTasks.Add(new MoveTo_Ammo(this.gameObject));

        getAmmo = new Sequence(newTasks);

        newTasks = new List<Task>();
        newTasks.Add(getAmmo);
        newTasks.Add(new MoveAgent_Do(player, agent, range));

        getAmmoOrInRange = new Selector(newTasks);

        newTasks = new List<Task>();
        newTasks.Add(new IntAboveMin_Check(ammo, 0));
        newTasks.Add(new FloatBelowMin_Check(Vector3.Distance(transform.position, player.transform.position), range));
        newTasks.Add(new Shoot_Do(this));

        shootAtPlayer = new Sequence(newTasks);

        newTasks = new List<Task>();
        newTasks.Add(shootAtPlayer);
        newTasks.Add(getAmmoOrInRange);

        attackPlayer = new Selector(newTasks);


        return attackPlayer;
    }
}

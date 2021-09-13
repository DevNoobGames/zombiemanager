using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    float lowestDistance;

    public float health = 100;

    public List<Targets> targetlist = new List<Targets>();

    float startSpeed;

    [Header("Attack")]
    public float range;
    bool isReloaded = true;
    public float reloadTime = 1;
    public float damage = 10;

    [System.Serializable]
    public class Targets
    {
        public GameObject target;
        public float length;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startSpeed = agent.speed;
        //targetchecker();
        InvokeRepeating("targetcheckersimple", 0, 2); //check closest target every 2 sec

        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().zombies.Add(gameObject);
        }
    }


    void Update()
    {
        if (target)
        {
            Vector3 targetPostition = new Vector3(target.transform.position.x,
                                    this.transform.position.y,
                                    target.transform.position.z);
            this.transform.LookAt(targetPostition);


            if (Vector3.Distance(target.transform.position, transform.position) < range) //if within range
            {
                agent.speed = 0;
                if (isReloaded)
                {
                    isReloaded = false;
                    StartCoroutine(reloadCour());
                    target.transform.SendMessage("gotHit", damage, SendMessageOptions.DontRequireReceiver);
                    //inflict damage
                }
            }
            else
            {
                agent.speed = startSpeed;
            }
        }
    }

    IEnumerator reloadCour()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloaded = true;
    }

    void targetcheckersimple()
    {
        lowestDistance = 99999999;
        foreach (GameObject trg in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dist = Vector3.Distance(transform.position, trg.transform.position);
            if (dist <= lowestDistance)
            {
                lowestDistance = dist;
                target = trg; //so it looks at its target
                agent.SetDestination(trg.transform.position);
            }
        }
    }

    public void gotHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}

/*
 
    void targetchecker()
    {
        foreach (GameObject trg in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Targets t = new Targets();
            t.target = trg;
            t.length = checkDist(trg);
            targetlist.Add(t);
        }
    }
    //Function not working?
    public float checkDist(GameObject trg)
    {
        agent.SetDestination(trg.transform.position);

        if (agent.CalculatePath(trg.transform.position, new NavMeshPath())) //check path
        {
            if (agent.pathStatus == NavMeshPathStatus.PathComplete) //can reach path?
            {
                return agent.remainingDistance;
            }
        }
        return 0;
    } */
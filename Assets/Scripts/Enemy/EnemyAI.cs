using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public float MoneyReward = 100;
    PlayerManager playerMan;
    public GameManager gameMan; //is refered to when instantiaing in the gamemanager script
    public float Yfix = 0;

    public GameObject cam;

    [Header("Shooting")]
    public float reloadTime = 1;
    public float damage = 10;
    bool isReloaded = true;
    public Transform lineOrigin;

    [Header("Random")]
    public LineRenderer lineRender;
    public GameObject zombieTarget;
    public float health = 100;
    public ParticleSystem gunEffect;
    public AudioSource shotSound;
    public GameObject rewardText;

    void Start()
    {
        playerMan = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        float randVal = Random.Range(1.5f, 2.1f);
        InvokeRepeating("targetFinder", randVal, randVal);
        lineRender.SetPosition(0, lineOrigin.position);

        transform.position = new Vector3(transform.position.x, transform.position.y + Yfix, transform.position.z);
    }

    private void Update()
    {
        if (zombieTarget)
        {
            cam.transform.LookAt(zombieTarget.transform);

            /*Quaternion OriginalRot = transform.rotation;
            transform.LookAt(zombieTarget.transform);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, 1 * Time.deltaTime);*/


            lineRender.SetPosition(0, lineOrigin.position);
            lineRender.SetPosition(1, zombieTarget.transform.position);

            Vector3 targetPostition = new Vector3(zombieTarget.transform.position.x,
                this.transform.position.y,
                zombieTarget.transform.position.z);
            this.transform.LookAt(targetPostition);

            if (isReloaded)
            {
                isReloaded = false;
                gunEffect.Play();
                shotSound.Play();
                StartCoroutine(reloadCour());
                zombieTarget.GetComponent<ZombieAI>().gotHit(damage);
            }
        }
        else
        {
            lineRender.SetPosition(1, transform.position);
        }
    }

    public void gotHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject reward = Instantiate(rewardText, transform.position, Quaternion.identity);
            reward.transform.rotation = Camera.main.transform.rotation;
            reward.GetComponent<TextMeshPro>().text = "$" + MoneyReward;
            playerMan.ChangeMoney(MoneyReward);

            foreach (GameObject thisobj in gameMan.enemies)
            {
                if (thisobj == gameObject)
                {
                    gameMan.enemies.Remove(thisobj);
                    gameMan.checkEnemiesLeft();

                    if (cam.activeInHierarchy)
                    {
                        GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>().mainCamSel();
                    }

                    Destroy(gameObject);
                    return;
                }
            }

            //if can't be found for some weird reason
            gameMan.checkEnemiesLeft();
            Destroy(gameObject);
        }
    }

    public void targetFinder()
    {
        zombieTarget = nearestZombie();
    }


    IEnumerator reloadCour()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloaded = true;
    }

    public GameObject nearestZombie()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, (go.transform.position - transform.position), out hit))
            {
                if (hit.transform == go.transform)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }

        }
        return closest;
    }


    
}



//Deleted the fleeing. Too much hassle
/*public NavMeshAgent agent;
public Vector3 startPos;
public bool isFleeing;
public UnityMovementAI.FleeUnit fleeunit;
public UnityMovementAI.SteeringBasics steeringbasics;
public RunningDistance runningdistanceRef; */


//START FUNCTION
//startPos = transform.position;
//fleeunit = GetComponent<UnityMovementAI.FleeUnit>();
//agent = GetComponent<NavMeshAgent>();

//UPDATE FUNCTION
/*if (isFleeing && runningdistanceRef.inCircle.Count == 0)
{
    fleeunit.stop();
    fleeunit.enabled = false;
    isFleeing = false;
    agent.enabled = true;
    agent.SetDestination(startPos);
}*/
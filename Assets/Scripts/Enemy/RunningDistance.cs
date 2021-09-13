using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningDistance : MonoBehaviour
{
    public EnemyAI parentAI;

    public List<GameObject> inCircle = new List<GameObject>();

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            inCircle.Add(other.gameObject);
            parentAI.fleeunit.target = other.gameObject.transform;
            parentAI.fleeunit.enabled = true;
            parentAI.isFleeing = true;
            parentAI.agent.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            inCircle.Remove(other.gameObject);
        }
    }*/
}

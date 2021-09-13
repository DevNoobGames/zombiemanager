using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePositioning : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(00, Random.Range(0, 360), 0));
        transform.position = new Vector3(transform.position.x, Random.Range(-3f, 0.36f), transform.position.z);
    }
}

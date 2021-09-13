using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed;
    public float scrollSpeed;

    public float MaxUp;
    public float MaxDown;

    public float minDistance;
    public float maxDistance;

    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + moveSpeed, transform.eulerAngles.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - moveSpeed, transform.eulerAngles.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (transform.eulerAngles.x <= MaxUp)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + moveSpeed, transform.eulerAngles.y, transform.eulerAngles.z );
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (transform.eulerAngles.x >= MaxDown)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - moveSpeed, transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //zoom in
        {
            if (Vector3.Distance(Camera.main.transform.position, this.transform.position) > minDistance)
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, this.transform.position, (scrollSpeed));
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //zoom out
        {
            if (Vector3.Distance(Camera.main.transform.position, this.transform.position) < maxDistance)
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, this.transform.position, (-scrollSpeed));
            }
        }


    }
}

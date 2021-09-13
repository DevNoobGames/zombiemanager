using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed;
    public int activePos;
    public Vector3[] position;

    private Vector3 currentAngle;
    public Vector3[] targetAngle;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, position[activePos], Time.deltaTime * speed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngle[activePos].x, targetAngle[activePos].y, transform.rotation.z), Time.deltaTime * speed);



        if (Input.GetKeyDown(KeyCode.E))
        {
            if (activePos == 0)
            {
                activePos = (position.Length - 1);
            }
            else
            {
                activePos--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (activePos == (position.Length - 1))
            {
                activePos = 0;
            }
            else
            {
                activePos++;
            }
        }
    }
}

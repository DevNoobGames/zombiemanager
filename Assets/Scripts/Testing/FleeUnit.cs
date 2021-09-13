using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMovementAI
{
    public class FleeUnit : MonoBehaviour
    {
        public Transform target;

        SteeringBasics steeringBasics;
        Flee flee;

        private void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            flee = GetComponent<Flee>();
        }

        public void stop()
        {
            steeringBasics.maxVelocity = 0;
            Vector3 accel = flee.GetSteering(transform.position);
            steeringBasics.Steer(accel);
        }

        private void FixedUpdate()
        {
            if (target)
            {
                steeringBasics.maxVelocity = 3.5f;
                Vector3 accel = flee.GetSteering(target.position);

                steeringBasics.Steer(accel);
                steeringBasics.LookWhereYoureGoing();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(MovementAIRigidbody))]
    public class Flee : MonoBehaviour
    {
        public float panicDist = 3.5f;

        public bool decelerateOnStop = true;

        public float maxAcceleration = 10f;

        public float timeToTarget = 0.1f;

        MovementAIRigidbody rb;

        private void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
        }

        public Vector3 GetSteering(Vector3 targetPosition)
        {
            Vector3 acceleration = transform.position - targetPosition;

            if (acceleration.magnitude > panicDist)
            {
                if (decelerateOnStop && rb.Velocity.magnitude > 0.001f)
                {
                    acceleration = -rb.Velocity / timeToTarget;

                    if (acceleration.magnitude > maxAcceleration)
                    {
                        acceleration = GiveMaxAccel(acceleration);
                    }

                    return acceleration;
                }
                else
                {
                    rb.Velocity = Vector3.zero;
                    return Vector3.zero;
                }
            }

            return GiveMaxAccel(acceleration);
        }

        Vector3 GiveMaxAccel(Vector3 v)
        {
            v.Normalize();

            v *= maxAcceleration;

            return v;
        }
    }
}

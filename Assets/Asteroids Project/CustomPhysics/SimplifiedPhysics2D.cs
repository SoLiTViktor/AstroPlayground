using UnityEngine;

namespace AsteroidProject
{
    public class SimplifiedPhysics2D
    {
        private float EnvironmentDrag { get; } = 1f;

        private float _minBreakTime = 0.02f;
        private float _maxBreakingTime = 0.1f;

        private float _epsilon = 0.001f;

        //private float _crushForceRatio = 0.1f;

        public Vector2 RetardMoving(Vector2 velocity, float mass)
        {
            float breakTime = (velocity.magnitude * EnvironmentDrag) / mass;
            breakTime = Mathf.Clamp(breakTime, _minBreakTime, _maxBreakingTime);
            velocity = Vector2.Lerp(velocity, Vector2.zero, breakTime);

            return velocity.magnitude > _epsilon ? velocity : Vector2.zero;
        }

        public float RetardRotation(float torque, Vector2 velocity, float mass)
        {
            float breakTime = Mathf.Abs(torque) / EnvironmentDrag;
            breakTime = Mathf.Clamp(breakTime, _minBreakTime, _maxBreakingTime / 2);
            torque = Mathf.Lerp(torque, 0, breakTime);

            return Mathf.Abs(torque) > _epsilon ? torque : 0;
        }

        public Vector2 CalulateCrushForce(SimplifiedBody2D mainBody, SimplifiedBody2D pushedBody)
        {
            Vector2 centersVector = pushedBody.transform.position - mainBody.transform.position;

            float massRatio = mainBody.Mass > pushedBody.Mass ?
                pushedBody.Mass / mainBody.Mass :
                mainBody.Mass / pushedBody.Mass;

            Vector2 pushingVector = mainBody.Velocity * massRatio + centersVector.normalized * massRatio;


            return pushingVector;
        }
    }
}

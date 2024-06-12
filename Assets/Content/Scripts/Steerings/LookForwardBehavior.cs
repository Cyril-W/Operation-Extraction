using UnityEngine;

public class LookForwardBehavior : Steering {
    public override SteeringData GetSteering() { 
        var steering = new SteeringData();
            if (steeringBehavior) {
                var velocity = steeringBehavior.GetVelocity();
                if (velocity.magnitude == 0) {
                    return steering;
                }
                var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
                steering.angular = Mathf.LerpAngle(steeringBehavior.Orientation, angle, steeringBehavior.MaxAngularAcceleration * Time.deltaTime);
                steering.linear = Vector3.zero;
            }
        return steering;
    }
}

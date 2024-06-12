using UnityEngine;

public class SeekBehavior : Steering {
    public override SteeringData GetSteering() { 
        var steering = new SteeringData();
        steering.linear = targetPosition - selfPosition;
        steering.linear.Normalize();
        if (steeringBehavior) {
            steering.linear *= steeringBehavior.MaxAcceleration;
            steering.angular = 0;
        }
        return steering;
    }
}

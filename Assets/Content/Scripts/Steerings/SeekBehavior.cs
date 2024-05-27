using UnityEngine;

public class SeekBehavior : Steering {
    [Space]
    [SerializeField] Transform self;
    [SerializeField] Transform target;

    public override SteeringData GetSteering(SteeringBehavior steeringBehavior) { 
        var steering = new SteeringData();
        if (self && target) {
            steering.linear = target.position - self.position;
            steering.linear.Normalize();
        }
        if (steeringBehavior) {
            steering.linear *= steeringBehavior.MaxAcceleration;
            steering.angular = 0;
        }
        return steering;
    }
}

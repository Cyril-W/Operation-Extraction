using UnityEngine;

public class LookForwardBehavior : Steering {
    [Space]
    [SerializeField] Transform self;

    public override SteeringData GetSteering(SteeringBehavior steeringBehavior) { 
        var steering = new SteeringData();
            if (steeringBehavior) {
                var velocity = steeringBehavior.GetVelocity();
                if (velocity.magnitude == 0) {
                    return steering;
                }
            var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg; 
            steering.angular = Mathf.LerpAngle(self.rotation.eulerAngles.y, angle, steeringBehavior.MaxAngularAcceleration * Time.deltaTime); 
            steering.linear = Vector3.zero;
        }
        return steering;
    }
}

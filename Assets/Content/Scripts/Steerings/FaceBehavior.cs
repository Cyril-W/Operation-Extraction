using UnityEngine;

public class FaceBehavior : Steering {
    [Space]
    [SerializeField] Transform self;
    [SerializeField] Transform target;

    public override SteeringData GetSteering(SteeringBehavior steeringBehavior) { 
        var steering = new SteeringData();
        if (self && target) {
            var direction = target.position - self.position;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (steeringBehavior) {
                steering.angular = Mathf.LerpAngle(transform.rotation.eulerAngles.y, angle, steeringBehavior.MaxAngularAcceleration * Time.deltaTime);
                steering.linear = Vector3.zero;
            }
        }
        return steering;
    }
}

using UnityEngine;

public class ArriveBehavior : Steering {
    [Space]
    [SerializeField] Transform self;
    [SerializeField] Transform target;
    [Space]
    [SerializeField] float targetRadius = 1.5f;
    [SerializeField] float slowRadius = 5f;

    public override SteeringData GetSteering(SteeringBehavior steeringBehavior) {
        var steering = new SteeringData();
        if (self && target) {
            var direction = target.position - self.position; 
            var distance = direction.magnitude; 
            if (distance < targetRadius) {
                steeringBehavior.SetVelocity(Vector3.zero);
                return steering; 
            }
            if (steeringBehavior) {
                float targetSpeed;
                if (distance > slowRadius) {
                    targetSpeed = steeringBehavior.MaxAcceleration;
                } else {
                    targetSpeed = steeringBehavior.MaxAcceleration * (distance / slowRadius);
                }
                var targetVelocity = direction;
                targetVelocity.Normalize();
                targetVelocity *= targetSpeed;
                steering.linear = targetVelocity - steeringBehavior.GetVelocity();
                if (steering.linear.magnitude > steeringBehavior.MaxAcceleration) {
                    steering.linear.Normalize();
                    steering.linear *= steeringBehavior.MaxAcceleration;
                }
                steering.angular = 0;
                return steering;
            }
        }        
        return steering;
    }

    void OnDrawGizmosSelected() {
        if (target) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position, targetRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.position, slowRadius);
        }
    }
}

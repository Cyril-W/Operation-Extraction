using UnityEngine;

public class ArriveBehavior : Steering {
    [Space]
    [SerializeField] float targetRadius = 1.5f;
    [SerializeField] Color targetColor = Color.green;
    [SerializeField] float slowRadius = 5f;
    [SerializeField] Color slowColor = Color.yellow;

    public override SteeringData GetSteering() {
        var steering = new SteeringData();
        var direction = targetPosition - selfPosition; 
        var distance = direction.magnitude; 
        if (steeringBehavior) {
            if (distance < targetRadius) {
                steeringBehavior.SetVelocity(Vector3.zero);
                return steering; 
            }
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
        return steering;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = targetColor;
        Gizmos.DrawWireSphere(targetPosition, targetRadius);
        Gizmos.color = slowColor;
        Gizmos.DrawWireSphere(targetPosition, slowRadius);
    }
}

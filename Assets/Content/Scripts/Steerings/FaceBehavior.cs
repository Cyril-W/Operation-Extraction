using UnityEngine;

public class FaceBehavior : Steering {
    [Space]
    [SerializeField] Color lineColor = Color.magenta;

    public override SteeringData GetSteering() {
        var steering = new SteeringData();
        var direction = correctedTargetPosition - selfPosition;
        var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (steeringBehavior) {
            steering.angular = Mathf.LerpAngle(steeringBehavior.Orientation, angle, steeringBehavior.MaxAngularAcceleration * Time.deltaTime);
            steering.linear = Vector3.zero;
        }
        return steering;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = lineColor;
        Gizmos.DrawLine(correctedTargetPosition, selfPosition);
    }
}

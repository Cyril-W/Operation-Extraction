using UnityEngine;

public class WanderBehavior : Steering {
    [Space]
    [SerializeField]bool wanderFromSelf = false;
    [SerializeField] Vector2 wanderInterval = new Vector2(1f, 2f);
    //[SerializeField] float wanderOffset = 1.5f;
    [SerializeField] float wanderRadius = 4f;
    [SerializeField] Color wanderColor = Color.red;

    float lastWander = 0f;
    //float wanderOrientation = 0f;
    Vector3 newTargetPosition;

    /*float RandomBinomial() {
        return Random.value - Random.value;
    }
    Vector3 OrientationToVector(float orientation) { 
        return new Vector3(Mathf.Cos(orientation), 0, Mathf.Sin(orientation));
    }*/

    public override SteeringData GetSteering() {
        var steering = new SteeringData();
        lastWander -= Time.deltaTime;
        if (lastWander <= 0f) {
            lastWander = Random.Range(wanderInterval.x, wanderInterval.y);
            //wanderOrientation += RandomBinomial() * wanderFrequency;
            //var characterOrientation = self.rotation.eulerAngles.y * Mathf.Deg2Rad;
            //var targetOrientation = wanderOrientation + characterOrientation;
            var spherePosition = Random.insideUnitCircle * wanderRadius;
            newTargetPosition = (wanderFromSelf ? selfPosition : targetPosition) + new Vector3(spherePosition.x, 0f, spherePosition.y); //(wanderOffset * OrientationToVector(characterOrientation));
            //targetPosition += wanderRadius * OrientationToVector(targetOrientation);
            if (steeringBehavior) {
                steeringBehavior.SetCorrectedTarget(newTargetPosition);
            }
        }      
        steering.linear = newTargetPosition - selfPosition;
        steering.linear.Normalize();
        if (steeringBehavior) {
            steering.linear *= steeringBehavior.MaxAcceleration;
        }
        return steering;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = wanderColor;
        Gizmos.DrawSphere(newTargetPosition, 0.1f);
        Gizmos.DrawWireSphere(wanderFromSelf ? newTargetPosition : selfPosition, wanderRadius);
    }
}

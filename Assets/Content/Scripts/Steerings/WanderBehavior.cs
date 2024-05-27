using UnityEngine;

public class WanderBehavior : Steering {
    [Space]
    [SerializeField] Transform self;
    [SerializeField] Transform target;
    [Space]
    [SerializeField] Vector2 wanderInterval = new Vector2(1f, 2f);
    //[SerializeField] float wanderOffset = 1.5f;
    [SerializeField] float wanderRadius = 4f;

    float lastWander = 0f;
    //float wanderOrientation = 0f;
    Vector3 targetPosition;

    /*float RandomBinomial() {
        return Random.value - Random.value;
    }
    Vector3 OrientationToVector(float orientation) { 
        return new Vector3(Mathf.Cos(orientation), 0, Mathf.Sin(orientation));
    }*/

    public override SteeringData GetSteering(SteeringBehavior steeringBehavior) {
        var steering = new SteeringData();
        lastWander -= Time.deltaTime;
        if (lastWander <= 0f) {
            lastWander = Random.Range(wanderInterval.x, wanderInterval.y);
            //wanderOrientation += RandomBinomial() * wanderFrequency;
            if (target || self) {
                //var characterOrientation = self.rotation.eulerAngles.y * Mathf.Deg2Rad;
                //var targetOrientation = wanderOrientation + characterOrientation;
                var spherePosition = Random.insideUnitCircle * wanderRadius;
                targetPosition = (target ? target.position : self.position) + new Vector3(spherePosition.x, 0f, spherePosition.y); //(wanderOffset * OrientationToVector(characterOrientation));
                //targetPosition += wanderRadius * OrientationToVector(targetOrientation);
            }
        }
        if (self) { 
            steering.linear = targetPosition - self.position;
            steering.linear.Normalize();
        }
        if (steeringBehavior) {
            steering.linear *= steeringBehavior.MaxAcceleration;
        }
        return steering;
    }

    void OnDrawGizmosSelected() {
        if (target || self) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition, 0.1f);
            Gizmos.DrawWireSphere(target ? target.position : self.position, wanderRadius);
        }
    }
}

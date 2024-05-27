using UnityEngine;

public class SteeringData {
    public Vector3 linear;
    public float angular;

    public SteeringData() {
        linear = Vector3.zero;
        angular = 0f;
    }
}

public abstract class Steering : MonoBehaviour {
    [SerializeField] float weight = 1f;

    public abstract SteeringData GetSteering(SteeringBehavior steeringBehavior);

    public float GetWeight() {
        return weight;
    }
}

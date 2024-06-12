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

    protected SteeringBehavior steeringBehavior;
    protected Vector3 selfPosition;
    protected Vector3 targetPosition;
    protected Vector3 correctedTargetPosition;

    public void SetSelfTargets() {
        steeringBehavior = SteeringBehavior.Instance;
        if (!steeringBehavior) {
            Debug.LogError("No SteeringBehavior found");
            return;
        }
        selfPosition = steeringBehavior.SelfPosition;
        targetPosition = steeringBehavior.TargetPosition;
        correctedTargetPosition = steeringBehavior.CorrectedTargetPosition;
    }

    public abstract SteeringData GetSteering();

    public float GetWeight() {
        return weight;
    }
}

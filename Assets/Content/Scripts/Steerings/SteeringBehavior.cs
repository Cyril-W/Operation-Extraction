using UnityEngine;

public class SteeringBehavior : MonoBehaviour {
    public static SteeringBehavior Instance { get; private set; }

    public float MaxAcceleration { get { return maxAcceleration; } }
    public float MaxAngularAcceleration { get { return maxAngularAcceleratioon; } }
    public Vector3 SelfPosition { get { return self.position; } }
    public Vector3 TargetPosition { get { return target.position; } }
    public Vector3 CorrectedTargetPosition { get { return correctedTargetPosition; } }
    public float Orientation { get { return self.rotation.eulerAngles.y; } }

    [SerializeField] float maxAcceleration = 10f;
    [SerializeField] float maxAngularAcceleratioon = 3f;
    [SerializeField] float drag = 1f;
    [Space]
    [SerializeField] Transform self;
    [SerializeField] Transform target;
    [Space]
    [SerializeField] Steering[] steerings;

    Rigidbody rb;
    Vector3 correctedTargetPosition;

    void OnValidate() {
        if (steerings != null && steerings.Length == 0) {
            steerings = GetComponents<Steering>();
        }
    }

    void Start() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
    }

    public void SetVelocity (Vector3 newVelocity) {
        if (rb) {
            rb.velocity = newVelocity;
        }
    }

    public Vector3 GetVelocity() {
        if (rb) {
            return rb.velocity;
        }
        return Vector3.zero;
    }

    public void SetTarget(Vector3 newTargetPosition) {
        target.position = newTargetPosition;
    }

    public void SetCorrectedTarget(Vector3 newCorrectedTargetPosition) {
        correctedTargetPosition = newCorrectedTargetPosition;
    }

    void FixedUpdate() {
        var accelaration = Vector3.zero;
        var rotation = 0f;
        //correctedTargetPosition = target.position;
        foreach (Steering behavior in steerings) {
            behavior.SetSelfTargets();
            var steeringData = behavior.GetSteering();
            accelaration += steeringData.linear * behavior.GetWeight();
            rotation += steeringData.angular * behavior.GetWeight();
        }
        if (accelaration.magnitude > maxAcceleration) {
            accelaration.Normalize();
            accelaration *= maxAcceleration;
        }
        rb.AddForce(accelaration, ForceMode.VelocityChange);
        if (rotation != 0) {
            rb.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
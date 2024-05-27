using UnityEngine;

public class SteeringBehavior : MonoBehaviour {
    public float MaxAcceleration { get { return maxAcceleration; } }
    public float MaxAngularAcceleration { get { return maxAngularAcceleratioon; } }
    [SerializeField] float maxAcceleration = 10f;
    [SerializeField] float maxAngularAcceleratioon = 3f;
    [SerializeField] float drag = 1f;
    [Space]
    [SerializeField] Steering[] steerings;

    Rigidbody rb;

    void OnValidate() {
        if (steerings != null && steerings.Length == 0) {
            steerings = GetComponents<Steering>();
        }
    }

    void Start() {
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

    void FixedUpdate() {
        var accelaration = Vector3.zero;
        var rotation = 0f;
        foreach (Steering behavior in steerings) {
            var steering = behavior.GetSteering(this);
            accelaration += steering.linear * behavior.GetWeight();
            rotation += steering.angular * behavior.GetWeight();
        }
        if (accelaration.magnitude > maxAcceleration) {
            accelaration.Normalize();
            accelaration *= maxAcceleration;
        }
        rb.AddForce(accelaration);
        if (rotation != 0) {
            rb.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
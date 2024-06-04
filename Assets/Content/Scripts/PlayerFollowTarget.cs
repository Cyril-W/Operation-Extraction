using UnityEngine;

public class PlayerFollowTarget : MonoBehaviour {
    [SerializeField] Transform target;

    Rigidbody rb;

    void Start() {
        TryGetRB();
    }

    bool TryGetRB() {
        if (rb) { return true; }
        else {
            rb = GetComponent<Rigidbody>();
            if (rb) { return true; }
            else {
                Debug.LogError("No Rigidbody found");
                return false;
            }
        }
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    void OnEnable() {
        if (!TryGetRB()) { return; }
        rb.isKinematic = true;
    }
    void OnDisable() {
        if (!TryGetRB()) { return; }
        rb.isKinematic = false;
    }
    void Update() {
        if (!target || !TryGetRB()) { return; }
        rb.MovePosition(target.position);
    }
}

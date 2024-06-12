using UnityEngine;

public class TargetSetter : MonoBehaviour {
    [SerializeField] Transform target;
    [Space]
    [SerializeField] float verticalOffset = 0.5f;
    [Space]
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask layerMask;

    Vector3 mousePosition;

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (GetMousePosition()) {
                if (SteeringBehavior.Instance) { SteeringBehavior.Instance.SetTarget(mousePosition); }
                if (target) { target.position = mousePosition; }
            }
        }
    }

    bool GetMousePosition() {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask)) {
            mousePosition = hit.point;
            mousePosition.y += verticalOffset;
            return true;
        }
        return false;
    }
}

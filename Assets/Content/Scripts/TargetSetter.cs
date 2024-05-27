using UnityEngine;

public class TargetSetter : MonoBehaviour {
    [Space]
    [SerializeField] Transform target;
    [Space]
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask layerMask;

    Vector3 mousePosition;

    void FixedUpdate() {
        if (target && Input.GetMouseButton(0)) {
            if (GetMousePosition()) {
                target.position = mousePosition;
            }
        }
    }

    bool GetMousePosition() {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask)) {
            mousePosition = hit.point;
            mousePosition.y = 0;
            return true;
        }
        return false;
    }
}

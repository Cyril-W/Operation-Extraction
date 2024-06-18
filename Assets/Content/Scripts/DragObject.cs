using UnityEngine;

public class DragObject : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgentManager navMeshAgentManager;
    [Space]
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask layerMask;

    bool lastMouse = false;

    void FixedUpdate() {
        if (lastMouse == Input.GetMouseButton(0)) {
            return;
        }
        lastMouse = Input.GetMouseButton(0);
        if (lastMouse) {
            if (GetMousePosition()) {
                if (NavMeshAgentManager.Instance) {
                    NavMeshAgentManager.Instance.SetTarget(target);
                }
                if (navMeshAgentManager) {
                    navMeshAgentManager.enabled = true;
                }
            }
        }
        else {
            if (NavMeshAgentManager.Instance) {
                NavMeshAgentManager.Instance.RestoreTarget();
            }
            if (navMeshAgentManager) {
                navMeshAgentManager.enabled = false;
            }
        }
    }

    bool GetMousePosition() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask)) {            
            return hit.transform == transform;
        }
        return false;
    }
}

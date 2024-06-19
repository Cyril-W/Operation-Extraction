using UnityEngine;
using UnityEngine.AI;

public class DragObject : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float distanceToTarget = 0.1f;
    [SerializeField] Color distanceRightColor = Color.cyan;
    [SerializeField] Color distanceWrongColor = Color.yellow;
    [SerializeField] NavMeshObstacle navmeshObstacle;
    [SerializeField] NavMeshAgentManager navMeshAgentManager;
    [Space]
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask layerMask;

    bool lastMouse = false;
    bool dragging = false;

    void OnValidate() {
        if (!navmeshObstacle) { navmeshObstacle = GetComponentInChildren<NavMeshObstacle>(); }
        if (!navMeshAgentManager) { navMeshAgentManager = GetComponentInChildren<NavMeshAgentManager>(); }
    }

    void Start() {
        if (navMeshAgentManager) {
            navMeshAgentManager.enabled = false;
        }
        if (navmeshObstacle) {
            navmeshObstacle.enabled = true;
        }
    }

    void FixedUpdate() {
        if (dragging && NavMeshAgentManager.Instance) {
            var distance = Vector3.Distance(NavMeshAgentManager.Instance.transform.position, target.position);
            if (navMeshAgentManager) {
                navMeshAgentManager.enabled = distance < distanceToTarget;
            }
            if (navmeshObstacle) {
                navmeshObstacle.enabled = distance >= distanceToTarget;
            }
        }

        if (lastMouse == Input.GetMouseButton(0)) {
            return;
        }
        lastMouse = Input.GetMouseButton(0);
        if (lastMouse) {
            if (GetMousePosition()) {
                dragging = true;
                if (NavMeshAgentManager.Instance) {
                    NavMeshAgentManager.Instance.SetTarget(target);
                }
            }
        }
        else {
            dragging = false;
            if (NavMeshAgentManager.Instance) {
                NavMeshAgentManager.Instance.RestoreTarget();
            }
            if (navMeshAgentManager) {
                navMeshAgentManager.enabled = false;
            }
            if (navmeshObstacle) {
                navmeshObstacle.enabled = true;
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

    void OnDrawGizmos() {
        if (dragging && NavMeshAgentManager.Instance) {
            var distance = Vector3.Distance(NavMeshAgentManager.Instance.transform.position, target.position);
            Gizmos.color = distance < distanceToTarget ? distanceRightColor : distanceWrongColor;
            Gizmos.DrawWireSphere(target.position, distanceToTarget);
        }
    }
}

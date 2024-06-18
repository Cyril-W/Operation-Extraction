using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentManager : MonoBehaviour {
    public static NavMeshAgentManager Instance { get; private set; }

    [SerializeField] bool isPlayer = false;
    [SerializeField, ReadOnly] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float distance = 0.1f;
    [SerializeField] bool wander = false;
    [SerializeField] bool wanderFromSelf = false;
    [SerializeField] Vector2 wanderInterval = new Vector2(1f, 2f);
    [SerializeField] float wanderRadius = 1f;
    [SerializeField] Color wanderColor = Color.red;

    bool initialWander;
    float lastWander = 0f;
    Vector3 newTargetPosition;
    Transform initialTarget;

    void OnValidate() {
        if (!agent) { agent = GetComponent<NavMeshAgent>(); }
    }

    void Start() {
        if (!agent) { agent = GetComponent<NavMeshAgent>(); }
        if (isPlayer) { Instance = this; }
        initialTarget = target;
        initialWander = wander;
    }

    void OnEnable() {
        if (agent && !isPlayer) {
            agent.enabled = true;
        }
    }

    void OnDisable() {
        if (agent && !isPlayer) {
            agent.enabled = false;
        }
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
        wander = false;
    }

    public void RestoreTarget() {
        target = initialTarget;
        wander = initialWander;
    }

    void FixedUpdate() {
        if (!target || !agent) { return; }   

        if (Vector3.Distance(agent.transform.position, target.position) >= distance) {
            agent.SetDestination(target.position);
        } else if (wander) {
            if (lastWander <= 0f) {
                lastWander = Random.Range(wanderInterval.x, wanderInterval.y);
                var spherePosition = Random.insideUnitCircle * wanderRadius;
                newTargetPosition = (wanderFromSelf ? agent.transform.position : target.position) + new Vector3(spherePosition.x, 0f, spherePosition.y);
                target.position = newTargetPosition;
            } else {
                lastWander -= Time.deltaTime;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = wanderColor;
        Gizmos.DrawSphere(newTargetPosition, 0.1f);
        if (wanderFromSelf && target || !wanderFromSelf && agent) {
            Gizmos.DrawWireSphere(wanderFromSelf ? target.position : agent.transform.position, wanderRadius);
        }
    }
}

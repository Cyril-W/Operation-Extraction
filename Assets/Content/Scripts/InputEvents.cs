using UnityEngine;
using UnityEngine.Events;

public class InputEvents : MonoBehaviour {
    [SerializeField] KeyCode interactKey;
    [SerializeField] UnityEvent onInteractKey;
    [SerializeField] KeyCode backKey;
    [SerializeField] UnityEvent onBackKey;

    void OnEnable() {
        if (CanvasInputsFromWorld.Instance) {
            CanvasInputsFromWorld.Instance.RegisterTarget(transform);
        }
    }

    void OnDisable() {
        if (CanvasInputsFromWorld.Instance) {
            CanvasInputsFromWorld.Instance.RegisterTarget(null);
        }
    }

    void Update() {
        if (interactKey != KeyCode.None && Input.GetKeyDown(interactKey)) {
            onInteractKey?.Invoke();
        }
        if (backKey != KeyCode.None && Input.GetKeyDown(backKey)) {
            onBackKey?.Invoke();
        }
    }
}

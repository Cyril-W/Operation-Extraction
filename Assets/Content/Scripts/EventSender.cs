using UnityEngine;
using UnityEngine.Events;

public class EventSender : MonoBehaviour {
    [SerializeField] UnityEvent OnAwakeEvent;
    [SerializeField] UnityEvent OnStartEvent;
    [SerializeField] UnityEvent OnEnableEvent;
    [SerializeField] UnityEvent OnDisableEvent;
    [SerializeField] UnityEvent OnSendEvent;
    [SerializeField] UnityEvent OnSendEventIfEnabled;
    void Awake() {
        OnAwakeEvent?.Invoke();
    }
    void Start() {
        OnStartEvent?.Invoke();
    }
    void OnEnable() {
        OnEnableEvent?.Invoke();
    }
    void OnDisable() {
        OnDisableEvent?.Invoke();   
    }

    [ContextMenu("Send Event")]
    public void SendEvend()
    {
        OnSendEvent?.Invoke();
    }

    public void SendEventIfEnabled()
    {
        if (isActiveAndEnabled) OnSendEventIfEnabled?.Invoke();
    }
}

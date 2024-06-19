using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterAnimationTrigger : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] AnimationClip Animation;
    [SerializeField] float ExtraTime;
    [Header("Parameters")]
    [SerializeField] bool triggerOnEnable = false;
    [SerializeField] bool AutoDisableTrigger = false;
    [Header("Events")]
    public UnityEvent AfterDelayEvent;

    void OnEnable()
    {
        if (triggerOnEnable)
        {
            StartTrigger();
        }
    }

    void OnDisable()
    {
        StopTrigger();
    }

    public void StartTrigger()
    {
        if (gameObject.activeInHierarchy) StartCoroutine(WaitAndTrigger());
    }

    public void StopTrigger()
    {
        if (gameObject.activeInHierarchy) StopAllCoroutines();
    }

    public void SendEvent()
    {
        AfterDelayEvent?.Invoke();
    }

    IEnumerator WaitAndTrigger()
    {
        yield return new WaitForSeconds(Animation.length + ExtraTime);
        AfterDelayEvent?.Invoke();

        if (AutoDisableTrigger) enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField] Volume GlobalVolume;

    [SerializeField] bool DynamicDOF;

    DepthOfField dofComponent;

    void OnEnable()
    {
        DepthOfField tmp;

        if (GlobalVolume.profile.TryGet<DepthOfField>(out tmp))
        {
            dofComponent = tmp;
        }
    }

    void Update()
    {
        if (Player && DynamicDOF && dofComponent)
        {
            dofComponent.focusDistance.value = Vector3.Distance(transform.position, Player.transform.position);
        }
    }
}

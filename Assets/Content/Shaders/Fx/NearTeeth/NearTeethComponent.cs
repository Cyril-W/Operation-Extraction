using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/NearTeeth", typeof(UniversalRenderPipeline))]
public class NearTeethComponent : VolumeComponent, IPostProcessComponent
{
    // For example, an intensity parameter that goes from 0 to 1
    public ClampedFloatParameter intensity = new ClampedFloatParameter(value: 0, min: 0, max: 1, overrideState: true);

    public Vector3Parameter Direction = new Vector3Parameter(value: Vector3.zero, overrideState: true);

    // Tells when our effect should be rendered
    public bool IsActive() => intensity.value > 0;

    // I have no idea what this does yet but I'll update the post once I find an usage
    public bool IsTileCompatible() => true;
}

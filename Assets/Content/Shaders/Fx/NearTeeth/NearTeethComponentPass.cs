using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class NearTeethComponentPass : ScriptableRenderPass
{
    // Used to render from camera to post processings
    // back and forth, until we render the final image to
    // the camera
    RenderTargetIdentifier source;
    RenderTargetIdentifier destinationA;
    RenderTargetIdentifier destinationB;
    RenderTargetIdentifier latestDest;

    readonly int temporaryRTIdA = Shader.PropertyToID("_TempRT");
    readonly int temporaryRTIdB = Shader.PropertyToID("_TempRTB");

    public NearTeethComponentPass()
    {
        // Set the render pass event
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        // Grab the camera target descriptor. We will use this when creating a temporary render texture.
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = 0;

        var renderer = renderingData.cameraData.renderer;
        //source = renderer.cameraColorTarget;
        source = renderer.cameraColorTargetHandle;

        // Create a temporary render texture using the descriptor from above.
        cmd.GetTemporaryRT(temporaryRTIdA, descriptor, FilterMode.Bilinear);
        destinationA = new RenderTargetIdentifier(temporaryRTIdA);
        cmd.GetTemporaryRT(temporaryRTIdB, descriptor, FilterMode.Bilinear);
        destinationB = new RenderTargetIdentifier(temporaryRTIdB);
    }

    // The actual execution of the pass. This is where custom rendering occurs.
    [System.Obsolete]
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Skipping post processing rendering inside the scene View
        //if (renderingData.cameraData.isSceneViewCamera)
        //    return;

        Shader sh = Shader.Find("Shader Graphs/SG-NearTeeth");

        if (sh == null)
        {
            Debug.LogError("Custom Post Processing Shader instance is null");
            return;
        }

        Material material = new Material(sh);

        if (material == null)
        {
            Debug.LogError("Custom Post Processing Materials instance is null");
            return;
        }

        CommandBuffer cmd = CommandBufferPool.Get("Custom Post Processing");
        cmd.Clear();

        // This holds all the current Volumes information
        // which we will need later
        var stack = VolumeManager.instance.stack;

        // Starts with the camera source
        latestDest = source;

        //---Custom effect here---
        var customEffect = stack.GetComponent<NearTeethComponent>();
        // Only process if the effect is active
        if (customEffect.IsActive())
        {
            // P.s. optimize by caching the property ID somewhere else
            material.SetFloat(Shader.PropertyToID("_Intensity"), customEffect.intensity.value);
            material.SetVector(Shader.PropertyToID("_Direction"), customEffect.Direction.value);

            var first = latestDest;
            var last = first == destinationA ? destinationB : destinationA;

            Blit(cmd, first, last, material, 0);

            latestDest = last;
        }

        // Add any other custom effect/component you want, in your preferred order
        // Custom effect 2, 3 , ...


        // DONE! Now that we have processed all our custom effects, applies the final result to camera
        Blit(cmd, latestDest, source);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    //Cleans the temporary RTs when we don't need them anymore
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(temporaryRTIdA);
        cmd.ReleaseTemporaryRT(temporaryRTIdB);
    }
}

using UnityEngine.Rendering.Universal;

[System.Serializable]
public class NearTeethPostProcessRenderer : ScriptableRendererFeature
{
    NearTeethComponentPass pass;

    public override void Create()
    {
        pass = new NearTeethComponentPass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }
}

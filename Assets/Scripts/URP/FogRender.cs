using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FogRender : ScriptableRendererFeature
{
    private FogRenderPass _renderPass;

    private FogRenderPass pass = null;
    
    public override void Create()
    {
        pass = new FogRenderPass("lost_wall_frog");
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        pass.Setup(src);
        renderer.EnqueuePass(pass); 
    }
}

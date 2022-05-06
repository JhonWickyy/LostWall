using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FogRenderData : VolumeComponent, IPostProcessComponent
{
    [Tooltip("Controls the lightest portions of the render.")]
    public Vector4Parameter highlights = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f));

    public bool IsActive() => true;

    public bool IsTileCompatible() => true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FogRenderPass : ScriptableRenderPass
{
	public static readonly int MainTextRef = Shader.PropertyToID("_MainTex");

	public static readonly int ColorRef = Shader.PropertyToID("_Color");

	
	private Shader fogShader = null;

	private Material material = null;
	
	private RenderTargetIdentifier passSource { get; set; }
	
	string passTag;
	
	public FogRenderPass(string tag)
	{
		passTag = tag;
	}
	
	public void Setup(RenderTargetIdentifier sour)
	{
		this.passSource = sour;
		
		if(fogShader == null)
			fogShader = CookShaders.Find("Soda/Effect/GlobalFog");

		material = new Material(fogShader);
	}
	
	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		if (material == null)
			return;


		CommandBuffer cmd = CommandBufferPool.Get(passTag);

		

		cmd.Blit(passSource, passSource, material);
		context.ExecuteCommandBuffer(cmd);
		CommandBufferPool.Release(cmd);
	}
}

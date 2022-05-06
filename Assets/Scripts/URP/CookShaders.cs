using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CookShaders : MonoBehaviour
{
	public Action CookShadersCompleteCallback;

    public Shader[] shaders;

    private Dictionary<string, Shader> shaderMap = new Dictionary<string, Shader> ();
	public static CookShaders Instance;

	void Awake()
	{
		Instance = this;

		if (shaders == null) 
		{
			return;	
		}

		for (int i = 0; i < shaders.Length; i++) 
		{
            if(shaders[i] == null)
                continue;

            if (shaderMap.ContainsKey (shaders [i].name)) 
			{
				Debug.LogError(i + " : " + shaders[i].name);
				continue;
			}

			shaderMap.Add (shaders[i].name, shaders[i]);
		}
		
		DontDestroyOnLoad(this);
	}

	//TODO: preLoad shader
	public static Shader Find(string shaderName)
	{
        if (!Application.isPlaying) 
		{
			return Shader.Find (shaderName);
		}
		else 
		{
            if (Instance == null) {
				Debug.LogErrorFormat("CookShaders find \"{0}\" failed! Shaders wasn't inited!", shaderName);
				return null;
			}

			Shader shader = null;
			if (!Instance.shaderMap.TryGetValue (shaderName, out shader)) {
				shader = Shader.Find (shaderName);
				if (shader == null) {
					#if UNITY_EDITOR
					     shader = Shader.Find(shaderName);
						if(shader != null)
						{
							return shader;
						}
					#endif
					Debug.LogErrorFormat("CookShaders find & Shader find \"{0}\" failed!", shaderName);
				} 
			}
			return shader;
        }
    }

	public void SetShaders(Shader[] shaders)
	{
		this.shaders = shaders;
	}

	public void InitData(Action callback)
	{
		CookShadersCompleteCallback = callback;
		StartCoroutine(Cook());
	}

	IEnumerator Cook()
	{
        if (shaders != null)
		{
            Debug.LogFormat("Cook start.Shaders count:{0}", shaders.Length);

            if (shaders.Length > 0)
			{	
				Material m = new Material(shaders[0]);
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

				cube.transform.parent = transform;
				Vector3 pos = Vector3.zero;
				pos.z += 4.0f;
				cube.transform.localPosition = pos;

				for (int i = 0; i < shaders.Length; i++)
				{
					Shader s = shaders[i];
					if (s != null)
					{
						m.shader = s;
						cube.GetComponent<Renderer>().material = m;
					}
					yield return 0;
				}

				Destroy(m);
				Destroy(cube);
			}
            Release();
		}

	}

	private void Release()
	{
        Debug.Log("Cook completed");
        if (CookShadersCompleteCallback != null)
			CookShadersCompleteCallback();
		CookShadersCompleteCallback = null;
	}
}

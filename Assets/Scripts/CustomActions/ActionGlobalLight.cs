using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

public class ActionGlobalLight : AC.Action
{
// Declare properties here
    public override ActionCategory Category
    {
        get { return ActionCategory.Custom; }
    }

    public override string Title
    {
        get { return "Light Control"; }
    }

    public override string Description
    {
        get { return "This is a blank Action template."; }
    }

    // Declare variables here
    [SerializeField] public Color globalLightColor;
    [SerializeField] public Light2D globalLight;
    
    public override float Run()
    {
        if (!isRunning)
        {
            isRunning = true;

            globalLight.color = globalLightColor;
            
            return defaultPauseTime;
        }
        else
        {
            isRunning = false;
            return 0f;
        }
    }


    public override void Skip()
    {
        Run();
    }


#if UNITY_EDITOR

    public override void ShowGUI()
    {
        globalLightColor = EditorGUILayout.ColorField("LightColor", globalLightColor);
        globalLight = (Light2D) EditorGUILayout.ObjectField("LightTarget:", globalLight, typeof(Light2D), true);

    }


    public override string SetLabel()
    {
        // (Optional) Return a string used to describe the specific action's job.

        return string.Empty;
    }

#endif
}

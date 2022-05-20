using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;
using Menu = AC.Menu;

public class ActionGameObject : AC.Action
{
    public override ActionCategory Category
    {
        get { return ActionCategory.Custom; }
    }

    public override string Title
    {
        get { return "显示或隐藏"; }
    }

    public override string Description
    {
        get { return "This is a blank Action template."; }
    }

    // Declare variables here
    [SerializeField] public GameObject GameObject;
    public ChangeType ChangeType = ChangeType.Enable;
    public string MenuItem;

    public override float Run()
    {
        if (!isRunning)
        {
            isRunning = true;

            if (GameObject != null)
            {
                GameObject.SetActive(ChangeType == ChangeType.Enable);
            }

            if (!string.IsNullOrEmpty(MenuItem))
            {
                
            }
            
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
        GameObject = (GameObject) EditorGUILayout.ObjectField("GameObject to affect:", GameObject, typeof (GameObject), true);
        MenuItem = EditorGUILayout.TextArea("Change to make:", MenuItem);
        ChangeType = (ChangeType) EditorGUILayout.EnumPopup ("Change to make:", ChangeType);
    }


    public override string SetLabel()
    {
        // (Optional) Return a string used to describe the specific action's job.

        return string.Empty;
    }

#endif
}

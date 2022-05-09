using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;

public class ActionDialogue : AC.Action
{
    // Declare properties here
    public override ActionCategory Category { get { return ActionCategory.Custom; }}
    public override string Title { get { return "Dialogue"; }}
    public override string Description { get { return "Show Dialogue"; }}


    // Declare variables here
#if UNITY_EDITOR
    private InventoryManager inventoryManager;
#endif

    protected int roleIndex;
    protected InvItem role;
    protected string[] Slot = new[] {"1", "2", "3"};

    public float Time;
    public string Content;
    public int SlotIndex;

    public override float Run ()
    {
        

        return 0;
    }


    public override void Skip ()
    {
        /*
         * This function is called when the Action is skipped, as a
         * result of the player invoking the "EndCutscene" input.
         * 
         * It should perform the instructions of the Action instantly -
         * regardless of whether or not the Action itself has been run
         * normally yet.  If this method is left blank, then skipping
         * the Action will have no effect.  If this method is removed,
         * or if the Run() method call is left below, then skipping the
         * Action will cause it to run itself as normal.
         */

        Run ();
    }

		
#if UNITY_EDITOR

    public override void ShowGUI ()
    {
        if (inventoryManager == null && AdvGame.GetReferences().inventoryManager)
        {
            inventoryManager = AdvGame.GetReferences().inventoryManager;
        }
        // Action-specific Inspector GUI code here

        if (inventoryManager)
        {
            List<string> labelList = new List<string>();

            if (inventoryManager.items.Count > 0)
            {
                foreach (InvItem _item in inventoryManager.items)
                {
                    if (_item.label.StartsWith("role"))
                    {
                        labelList.Add (_item.label);
                    }
                }
            }
            
            roleIndex = EditorGUILayout.Popup ("角色:", roleIndex, labelList.ToArray());
            role = inventoryManager.items[roleIndex];
        }
        
        Content = EditorGUILayout.TextField ("内容:", Content);
        Time = EditorGUILayout.FloatField("持续时间：", Time);
        SlotIndex = EditorGUILayout.Popup("位置(1:左 2:中 3:右):", SlotIndex,  Slot);
    }
		

    public override string SetLabel ()
    {
        // (Optional) Return a string used to describe the specific action's job.
			
        return string.Empty;
    }

#endif

}

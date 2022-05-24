using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;

namespace AC
{
	[System.Serializable]
	public class ActionTombStone : AC.Action
	{
		// Declare properties here
		
		private const string menuName = "TombstoneRevealedUI";

		public override ActionCategory Category { get { return ActionCategory.Custom; }}
		public override string Title { get { return "TombStone"; }}
		public override string Description { get { return "Display Item Details"; }}
    
		[SerializeField] public string FinalTombStoneIndex;
		[SerializeField] public string OrgTombStoneIndex;
		private Menu menu = null;
		public override float Run()
		{
			if (!isRunning)
			{
				isRunning = true;

				if (menu == null)
					menu = PlayerMenus.GetMenuWithName(menuName);

				if (menu)
				{
					menu.TurnOn();
				}

				return defaultPauseTime;
			}
			else
			{
				menu.RuntimeCanvas.GetComponent<TombstoneRevealedUI>().SetFinalTombstone(OrgTombStoneIndex,FinalTombStoneIndex);
				isRunning = false;
				return 0f;
			}
		}

		public override void Skip ()
		{
			Run();
		}
		
#if UNITY_EDITOR
		
		public override void ShowGUI()
		{
			FinalTombStoneIndex = EditorGUILayout.TextField("End Slot:", FinalTombStoneIndex);
			OrgTombStoneIndex = EditorGUILayout.TextField("Org Slot:", OrgTombStoneIndex);
		}
#endif
	}	
}


using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;

namespace AC
{
	[System.Serializable]
	public class ActionPuzzle : AC.Action
	{
		// Declare properties here
		
		public enum UIType
		{
			TombstoneRevealedUI,
			HeadFragmentsUI	
		}

		public override ActionCategory Category { get { return ActionCategory.Custom; }}
		public override string Title { get { return "Puzzle"; }}
		public override string Description { get { return "Display Item Details"; }}
    
		[SerializeField] public string FinalTombStoneIndex;
		[SerializeField] public string OrgTombStoneIndex;
		[SerializeField] public UIType UI = UIType.TombstoneRevealedUI;
		private Menu menu = null;
		public override float Run()
		{
			if (!isRunning)
			{
				isRunning = true;

				if (menu == null)
				{
					menu = PlayerMenus.GetMenuWithName(UI.ToString());
				}

				if (menu)
				{
					menu.TurnOn();
				}

				return defaultPauseTime;
			}
			else
			{
				menu.RuntimeCanvas.GetComponent<PuzzleUI>().SetFinalTombstone(OrgTombStoneIndex,FinalTombStoneIndex, UI);
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
			UI = (UIType) EditorGUILayout.EnumPopup ("UI Type:", UI);
		}
#endif
	}	
}


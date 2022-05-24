/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2021
 *	
 *	"ActionTemplate.cs"
 * 
 *	This is a blank action template.
 * 
 */

using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	[System.Serializable]
	public class ActionDisplayItem : AC.Action
	{
		private const string menuName = "ItemDisplay";
		private const string showImg = "ImgItem";
		private const string showTxt = "TxtItemDescribe";

		private Menu menu = null;

		// Declare properties here
		public override ActionCategory Category { get { return ActionCategory.Custom; }}
		public override string Title { get { return "Display"; }}
		public override string Description { get { return "Display Item Details"; }}


		// Declare variables here
		[SerializeField]  public Sprite ShowImage;
		[SerializeField] public string Txt_describe;
		[SerializeField] public float AutoCloseTime = 4.0f;
		[SerializeField] public bool PauseWhenShow = true;
		

		public override float Run ()
		{
			if (!isRunning)
			{
				isRunning = true;
				
				if (menu == null)
					menu = PlayerMenus.GetMenuWithName(menuName);

				if (menu)
				{
					var menuElement_Img = (MenuGraphic) menu.GetElementWithName(showImg);
					menuElement_Img.SetNormalGraphicTexture(ShowImage.texture);

					var menuElement_Txt = (MenuLabel) menu.GetElementWithName(showTxt);
					menuElement_Txt.label = Txt_describe;

					menu.TurnOn();
				}

				return AutoCloseTime;
			}
			else
			{
				isRunning = false;
				return 0f;
			}
		}

		public override void SetLastResult(int _lastRunOutput)
		{
			base.SetLastResult(_lastRunOutput);
			if (menu) menu.TurnOff();
		}

		public override void Skip ()
		{
			Run();
		}

		
		#if UNITY_EDITOR

		public override void ShowGUI ()
		{
			ShowImage = (Sprite) EditorGUILayout.ObjectField("Item Image:", ShowImage, typeof(Sprite), false);
			Txt_describe = EditorGUILayout.TextField ("Show Content:", Txt_describe);
			AutoCloseTime = EditorGUILayout.FloatField("Auto Close Time：", AutoCloseTime);
			PauseWhenShow = EditorGUILayout.Toggle("PauseWhenEnable", PauseWhenShow);
		}
		

		public override string SetLabel ()
		{
			// (Optional) Return a string used to describe the specific action's job.

			return GetType().ToString();
		}

		#endif
		
	}

}
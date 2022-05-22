using System.Collections;
using System.Collections.Generic;
using AC;
using UnityEditor;
using UnityEngine;

namespace AC
{
	public class ActionChangeSprite : AC.Action
	{
		// Declare properties here
		public override ActionCategory Category
		{
			get { return ActionCategory.Custom; }
		}

		public override string Title
		{
			get { return "修改sprite"; }
		}

		public override string Description
		{
			get { return "Display Item Details"; }
		}

		[SerializeField] public Sprite ShowImage;
		[SerializeField] public GameObject SpriteTarget;

		public override float Run()
		{
			if (!isRunning)
			{
				isRunning = true;

				if (SpriteTarget != null)
				{
					SpriteTarget.GetComponent<SpriteRenderer>().sprite = ShowImage;
				}

				return defaultPauseTime;
			}
			else
			{
				isRunning = false;
				return 0f;
			}
		}

#if UNITY_EDITOR

		public override void ShowGUI()
		{
			ShowImage = (Sprite) EditorGUILayout.ObjectField("图片:", ShowImage, typeof(Sprite), false);
			SpriteTarget = (GameObject) EditorGUILayout.ObjectField("修改对象:", SpriteTarget, typeof(GameObject), true);
		}


		public override string SetLabel()
		{
			// (Optional) Return a string used to describe the specific action's job.

			return GetType().ToString();
		}

#endif
	}
}


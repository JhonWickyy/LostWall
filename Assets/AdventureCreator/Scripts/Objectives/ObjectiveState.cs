﻿/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2021
 *	
 *	"ObjectiveState.cs"
 * 
 *	Stores data for a state that an Objective can take.
 * 
 */

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	/** Stores data for a state that an Objective can take. */
	[System.Serializable]
	public class ObjectiveState : ITranslatable
	{

		#region Variables

		/** A unique identifier */
		public int ID;
		[SerializeField] protected string label;
		/** The translation ID for the label text, generated by the Speech Manager */
		public int labelLineID = -1;
		/** An description for the state */
		public string description;
		/** The translation ID for the description text, generated by the Speech Manager */
		public int descriptionLineID = -1;
		/** The type of state it is (Active, Complete, Fail) */
		public ObjectiveStateType stateType;

		#endregion


		#region Constructors

		public ObjectiveState (int _ID, string _label, ObjectiveStateType _stateType)
		{
			ID = _ID;
			stateType = _stateType;
			label = _label;
			labelLineID = -1;
			description = string.Empty;
			descriptionLineID = -1;
		}


		public ObjectiveState (int[] idArray)
		{
			stateType = ObjectiveStateType.Active;
			label = string.Empty;
			labelLineID = -1;
			description = string.Empty;
			descriptionLineID = -1;

			ID = 0;
			// Update id based on array
			foreach (int _id in idArray)
			{
				if (ID == _id)
					ID ++;
			}
		}

		#endregion


		#region PublicFunctions

		/**
		 * <summary>Gets the states's label text in a given language</summary>
		 * <param name = "languageNumber">The language index, where 0 = the game's default language</param>
		 * <returns>The label text</returns>
		 */
		public string GetLabel (int languageNumber = 0)
		{
			return KickStarter.runtimeLanguages.GetTranslation (label, labelLineID, languageNumber, GetTranslationType (0));
		}


		/**
		 * <summary>Gets the states's description text in a given language</summary>
		 * <param name = "languageNumber">The language index, where 0 = the game's default language</param>
		 * <returns>The description text</returns>
		 */
		public string GetDescription (int languageNumber = 0)
		{
			return KickStarter.runtimeLanguages.GetTranslation (description, descriptionLineID, languageNumber, GetTranslationType (0));
		}


		/**
		 * <summary>Checks if the state matches a given display type, as used by InventoryBox elements</summary>
		 * <param name = "displayType">The display type</param>
		 * <returns>True if the state matches</returns>
		 */
		public bool DisplayTypeMatches (ObjectiveDisplayType displayType)
		{
			switch (displayType)
			{
				case ObjectiveDisplayType.All:
					return true;

				case ObjectiveDisplayType.ActiveOnly:
					return (stateType == ObjectiveStateType.Active);

				case ObjectiveDisplayType.CompleteOnly:
					return (stateType == ObjectiveStateType.Complete);

				case ObjectiveDisplayType.FailedOnly:
					return (stateType == ObjectiveStateType.Fail);

				default:
					return false;
			}
		}

		#endregion


		#region GetSet

		/**
		* The states's label.  This will set the title to '(Untitled)' if empty.
		*/
		public string Label
		{
			get
			{
				if (string.IsNullOrEmpty (label))
				{
					label = "(Untitled)";
				}
				return label;
			}
			set
			{
				label = value;
			}
		}

		#endregion


		#if UNITY_EDITOR

		public void ShowGUI (string apiPrefix)
		{
			label = CustomGUILayout.TextField ("Label:", label, apiPrefix + ".label");
			if (labelLineID > -1)
			{
				EditorGUILayout.LabelField ("Speech Manager ID:", labelLineID.ToString ());
			}

			if (ID >= 2)
			{
				stateType = (ObjectiveStateType) CustomGUILayout.EnumPopup ("State type:", stateType, apiPrefix + ".stateType");
			}
			else
			{
				EditorGUILayout.LabelField ("State type: " + stateType.ToString ());
			}

			EditorGUILayout.BeginHorizontal ();
			CustomGUILayout.LabelField ("Description:", GUILayout.Width (140f), apiPrefix + ".description");
			EditorStyles.textField.wordWrap = true;
			description = CustomGUILayout.TextArea (description, GUILayout.MaxWidth (800f), apiPrefix + ".description");
			EditorGUILayout.EndHorizontal ();
			if (descriptionLineID > -1)
			{
				EditorGUILayout.LabelField ("Speech Manager ID:", descriptionLineID.ToString ());
			}
		}

		#endif


		#region ITranslatable

		public string GetTranslatableString (int index)
		{
			if (index == 0)
			{
				return label;
			}
			else
			{
				return description;
			}
		}


		public int GetTranslationID (int index)
		{
			if (index == 0)
			{
				return labelLineID;
			}
			else
			{
				return descriptionLineID;
			}
		}


		public AC_TextType GetTranslationType (int index)
		{
			return AC_TextType.Objective;
		}


		#if UNITY_EDITOR

		public void UpdateTranslatableString (int index, string updatedText)
		{
			if (index == 0)
			{
				label = updatedText;
			}
			else
			{
				description = updatedText;
			}
		}


		public int GetNumTranslatables ()
		{
			return 2;
		}


		public bool HasExistingTranslation (int index)
		{
			if (index == 0)
			{
				return (labelLineID > -1);
			}
			else
			{
				return (descriptionLineID > -1);
			}
		}



		public void SetTranslationID (int index, int _lineID)
		{
			if (index == 0)
			{
				labelLineID = _lineID;
			}
			else
			{
				descriptionLineID = _lineID;
			}
		}


		public string GetOwner (int index)
		{
			return string.Empty;
		}


		public bool OwnerIsPlayer (int index)
		{
			return false;
		}


		public bool CanTranslate (int index)
		{
			if (index == 0)
			{
				return !string.IsNullOrEmpty (label);
			}
			return !string.IsNullOrEmpty (description);
		}

		#endif

		#endregion

	}

}
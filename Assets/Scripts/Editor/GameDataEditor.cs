using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class GameDataEditor :  EditorWindow
{
	public GameData gameData;

	private string gameDataProjectFilePath = "/StreamingAssets/data.json";

	Vector2 scrollposition = Vector2.zero;

	[MenuItem ("Window/Game Data Editor")]
	static void Init()
	{
		EditorWindow.GetWindow (typeof(GameDataEditor)).Show ();

	}

	void OnGUI()
	{

		scrollposition = GUILayout.BeginScrollView (scrollposition, true, true); // w/o GUILayout.Width (100), GUILayout.Height (100)
		//GUILayout.Button ("aa", GUILayout.MinWidth (150), GUILayout.MinHeight (150));

		if (gameData != null) 
		{
			SerializedObject serializedObject = new SerializedObject (this);
			SerializedProperty serializedProperty = serializedObject.FindProperty ("gameData");
			EditorGUILayout.PropertyField (serializedProperty, true);

			serializedObject.ApplyModifiedProperties ();

			if (GUILayout.Button ("Save data"))
			{
				SaveGameData();
			}
		}

		if (GUILayout.Button ("Load data"))
		{
			LoadGameData();
		}

		GUILayout.EndScrollView ();
	}

	private void LoadGameData()
	{
		string filePath = Application.dataPath + gameDataProjectFilePath;

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			gameData = JsonUtility.FromJson<GameData> (dataAsJson);
		} else 
		{
			gameData = new GameData();
		}
	}

	private void SaveGameData()
	{

		string dataAsJson = JsonUtility.ToJson (gameData);

		string filePath = Application.dataPath + gameDataProjectFilePath;
		File.WriteAllText (filePath, dataAsJson);

	}
}
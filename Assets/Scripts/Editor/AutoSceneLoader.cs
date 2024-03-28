using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

namespace Homework.Editor
{
	[InitializeOnLoad]
	public static class AutoSceneLoader
	{
		private const string EntryScenePath = "Assets/Content/Scenes/Entry Scene.unity";
		private const string ActiveScenePrefKey = "activeScene";

		static AutoSceneLoader()
		{
			EditorApplication.playModeStateChanged += EditorApplicationOnPlayModeStateChanged;
		}

		private static void EditorApplicationOnPlayModeStateChanged(PlayModeStateChange obj)
		{
			if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
			{
				if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
				{
					return;
				}

				var path = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
				EditorPrefs.SetString(ActiveScenePrefKey, path);

				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					try
					{
						EditorSceneManager.OpenScene(EntryScenePath);
					}
					catch
					{
						Debug.LogError($"Cannot load scene {EntryScenePath}");
						EditorApplication.isPlaying = false;
					}
				}
				else
				{
					EditorApplication.isPlaying = false;
				}
			}


			if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
			{
				var path = EditorPrefs.GetString(ActiveScenePrefKey);

				try
				{
					EditorSceneManager.OpenScene(path);
				}
				catch
				{
					Debug.LogError($"Cannot load scene: {path}");
				}
			}
		}
	}
}
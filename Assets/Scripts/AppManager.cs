using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : Singleton<AppManager>
{
	private void Awake()
	{
		_instance = this;
		DontDestroyOnLoad(this);
	}

	public void OpenGameScene()
	{
		StartCoroutine(OpenSceneWithLoading("game", OnGameSceneLoaded));
	}

	private void OnGameSceneLoaded()
	{
		//probaly want to make this better 
		GameManager.instance.StartNewGame();
		//get gamemanager in scene
		//gamemanager.startscene
	}

	private IEnumerator OpenSceneWithLoading(string sceneName, Action callback) //to be changed
	{
		Scene current = SceneManager.GetActiveScene();
		//	AsyncOperation main = SceneManager.UnloadSceneAsync(current.name);
		AsyncOperation loading = SceneManager.LoadSceneAsync("loading");
		while (!loading.isDone)
		{
			yield return null;
		}
		AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		while (!scene.isDone)
		{
			yield return null;
		}
		SceneManager.UnloadScene("loading");

		callback?.Invoke();
	}


	internal void CloseApplication()
	{
		Application.Quit();
	}
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : Singleton<AppManager>
{
	private void Awake()
	{
		_instance = this;
	}

	public void OpenGameScene()
	{
		StartCoroutine(OpenSceneWithLoading("game", OnGameSceneLoaded));
	}

	private void OnGameSceneLoaded()
	{
		//TODO probaly want to make this better at some point
		GameSceneController.instance.StartNewGame(); 
	}

	private IEnumerator OpenSceneWithLoading(string sceneName, Action callback) //to be imporved
	{
		Scene current = SceneManager.GetActiveScene();
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

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [field:SerializeField] public PlayerHealthUI PlayerHealthUI { get; private set; }
    [SerializeField] private GameObject pauseMenu;

	IEnumerator WaitForSecondsToLoadScene(float seconds, int sceneNumber)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator WaitForSecondsToPause(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Time.timeScale = 0;
	}

    public void StartGame()
    {
        StartCoroutine(WaitForSecondsToLoadScene(0.25f, 1));
    }

    public void LoadLevel(int level)
    {
		Time.timeScale = 1;
		StartCoroutine(WaitForSecondsToLoadScene(0.25f, (level + 1)));
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
		pauseMenu.SetActive(true);
         Time.timeScale = 0;
		//StartCoroutine(WaitForSecondsToPause(.5f));
	}

	public void Resume()
    {
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
}

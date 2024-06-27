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
    public void StartGame()
    {
        StartCoroutine(WaitForSecondsToLoadScene(0.25f, 1));
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(WaitForSecondsToLoadScene(0.25f, (level + 1)));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu()
    {
        Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}

	public void Resume()
    {
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
}

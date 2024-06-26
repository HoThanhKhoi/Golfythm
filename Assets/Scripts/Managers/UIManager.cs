using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [field:SerializeField] public PlayerHealthUI PlayerHealthUI { get; private set; }

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
}

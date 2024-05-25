using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    IEnumerator WaitForSecondsToLoadScene(float seconds, int sceneNumber)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(sceneNumber);
    }
    public void StartGame()
    {
        StartCoroutine(WaitForSecondsToLoadScene(0.75f, 1));
    }
}

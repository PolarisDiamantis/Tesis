using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void ChangeEscene(string nameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);
    }

    public void ChangeSceneToLevel(string nameScene)
    {
        Time.timeScale = 1f;
        if(ThemeSong.Instance != null)
        {
            ThemeSong.DestroyThemeSong();
        }
        SceneManager.LoadScene(nameScene);
    }

    public void RechargeScene()
    {
        string currentNameScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentNameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
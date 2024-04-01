using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelStart()
    {

    }
    public void UnlimitedPlay()
    {
        SceneManager.LoadScene("UnlimitedGame", LoadSceneMode.Single);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

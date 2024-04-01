using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void UnlimitedPlay()
    {
        SceneManager.LoadScene("UnlimitedGame", LoadSceneMode.Single);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

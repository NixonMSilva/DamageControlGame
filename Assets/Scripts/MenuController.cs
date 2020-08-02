using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject instructionBG;
    public GameObject mainButtons;

    public void PlayGame ()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void ShowInstructions ()
    {
        instructionBG.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void HideInstructions ()
    {
        instructionBG.SetActive(false);
        mainButtons.SetActive(true);
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    public void ReturnMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

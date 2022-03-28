using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private LevelLoader loader;

    private void Update()
    {
        OpenPauseMenu();
        if (pauseMenu.gameObject.activeSelf)
        {
            ClosePauseMenu();
        }
    }
    private void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ClosePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.gameObject.SetActive(false);
        }
    }
    public void LoadMainMenu()
    {
        loader.LoadLevel(1);
    }
}

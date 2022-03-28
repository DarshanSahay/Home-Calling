using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject respawnPanel;

    public event Action openPausePanel;
    public event Action openRespawnPanel;

    private void Start()
    {
        openPausePanel += OpenPauseMenu;
        openRespawnPanel += OpenRespawnPanel;
    }
    private void SetScreen(GameObject screen)
    { 
        screen.gameObject.SetActive(true);            // enable the requested screen
    }
    private void OpenPauseMenu()
    {
        SetScreen(pausePanel);
    }
    private void OpenRespawnPanel()
    {
        SetScreen(respawnPanel);
    }
    public void OnPlayerDeath()
    {
        openRespawnPanel?.Invoke();
    }
    public void OnPausing()
    {
        openPausePanel?.Invoke();
    }
}

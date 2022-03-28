using UnityEngine;
using UnityEngine.Playables;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private PlayableDirector mainMenuTimeline;
    [SerializeField] private PlayableDirector optionMenuTimeline;
    [SerializeField] private PlayableDirector cameraTimeline;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject optionCanvas;
    [SerializeField] private GameObject characterSelectionMenu;
    [SerializeField] private GameObject[] getConfirmationMessage;

    void Start()
    {
        optionCanvas.gameObject.SetActive(false);
    }
    public void ShowOptions()
    {
        optionMenuTimeline.Play();
        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }
    public void BackButton()
    {
        optionCanvas.gameObject.SetActive(false);
        cameraTimeline.Play();
        Invoke("PlayMainTimeLine", 0.9f);
    }
    public void PlayMainTimeLine()
    {
        optionMenuTimeline.Stop();
        mainMenuTimeline.Play();
        menuCanvas.gameObject.SetActive(true);
    }
    public void ActivateCharacterMenu()
    {
        characterSelectionMenu.gameObject.SetActive(true);
    }
    public void Activate_1ConfirmationMenu()
    {
        getConfirmationMessage[0].gameObject.SetActive(true);
    }
    public void Activate_2ConfirmationMenu()
    {
        getConfirmationMessage[1].gameObject.SetActive(true);
    }
    public void Close_1ConfirmationMenu()
    {
        getConfirmationMessage[0].gameObject.SetActive(false);
    }
    public void Close_2ConfirmationMenu()
    {
        getConfirmationMessage[1].gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

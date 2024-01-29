using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public PlaySoundManager soundManager;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.StartGame();
    }
    public void PauseGame()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.PauseGame();
    }
    public void BackMainMenu()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.BackMainMenu();
    }
    public void ResumeGame()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.ResumeGame();
    }
    public void RestartGame()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.RestartGame();
    }
    public void SettingMenu()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.ToggleSettings();
    }
    public void ResetRecord()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.ResetRecord();
    }
    public void QuitGame()
    {
        soundManager.buttonSoundPlay();
        GameManager.Instance.QuitGame();
    }
}

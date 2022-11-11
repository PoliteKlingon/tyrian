using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuView : AView
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button settingsButton;
    public override void Initialize()
    {
        resumeButton.onClick.AddListener(() =>
        {
            DoHide();
            PauseManager.ResumeGame();
        });
        
        settingsButton.onClick.AddListener(() =>
        {
            UIManager.Show<SettingsMenuView>();
        });
        
        returnButton.onClick.AddListener(() =>
        {
            PauseManager.ResumeGame();
            DoHide();
            ScoreManager.Instance.ResetScore();
            UIManager.GetView<HUDView>().DoHide();
            SceneManager.LoadScene("Game Manager");
            UIManager.Show<MainMenuView>();
        });
    }
}

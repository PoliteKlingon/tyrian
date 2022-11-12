using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuView : AView
{
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private TMP_Text scoreField;
    
    public override void Initialize()
    {
        levelsButton.onClick.AddListener(() =>
        {
            PauseManager.ResumeGame(hidePauseMenu:false);
            DoHide();
            UIManager.GetView<HUDView>().DoHide();
            UIManager.GetView<WinMenuView>().DoHide();
            SceneManager.LoadScene("Game Manager");
            UIManager.Show<LevelsMenuView>();
        });
        
        returnButton.onClick.AddListener(() =>
        {
            PauseManager.ResumeGame(hidePauseMenu:false);
            DoHide();
            UIManager.GetView<HUDView>().DoHide();
            SceneManager.LoadScene("Game Manager");
            UIManager.Show<MainMenuView>();
        });
    }

    public void ShowScore()
    {
        scoreField.text = "Your score was " + ScoreManager.Instance.CurrentScore();
    }
}
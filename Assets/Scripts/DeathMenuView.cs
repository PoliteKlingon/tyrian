using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuView : AView
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private TMP_Text scoreField;
    
    public override void Initialize()
    {
        restartButton.onClick.AddListener(() =>
        {
            DoHide();
            PauseManager.ResumeGame(hidePauseMenu:false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            UIManager.Show<HUDView>();
        });
        
        returnButton.onClick.AddListener(() =>
        {
            DoHide();
            PauseManager.ResumeGame(hidePauseMenu:false);
            SceneManager.LoadScene("Game Manager");
            UIManager.Show<MainMenuView>();
        });
    }

    public void ShowScore()
    {
        scoreField.text = "Your score was " + ScoreManager.Instance.CurrentScore();
    }
}
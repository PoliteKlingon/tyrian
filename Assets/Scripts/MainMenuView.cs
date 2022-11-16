using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : AView
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    public override void Initialize()
    {
        settingsButton.onClick.AddListener(() =>
        {
            UIManager.Show<SettingsMenuView>();
        });
        
        creditsButton.onClick.AddListener(() =>
        {
            UIManager.Show<CreditsMenuView>();
        });
        
        startButton.onClick.AddListener(() =>
        {
            UIManager.Show<LevelsMenuView>();
        });
        
        exitButton.onClick.AddListener(() =>
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        });
    }
}

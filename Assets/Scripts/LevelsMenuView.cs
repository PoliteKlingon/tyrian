using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuView : AView
{
    [SerializeField] private Button lvl1Button;
    [SerializeField] private Button lvl2Button;
    [SerializeField] private Button lvl3Button;
    [SerializeField] private Button backButton;

    //Why can't I use this?
    /*private void _genericListener(System.String sceneName)
    {
        DoHide();
        SceneManager.LoadScene(sceneName);
        UIManager.Show<HUDView>();
        
        ScoreManager.Instance.ResetScore();
        MeteorFactory.Instance.ResetMeteorsToDestroy();
        EnemyFactory.Instance.RestartEnemiesToKill();
        PauseManager.ResumeGame();
    }*/
    
    public override void Initialize()
    {
        backButton.onClick.AddListener(() =>
        {
            DoHide();
            UIManager.Show<MainMenuView>();
        });
        
        lvl1Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl1");
            UIManager.Show<HUDView>();
        
            ScoreManager.Instance.ResetScore();
        });
        
        lvl2Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl2");
            UIManager.Show<HUDView>();
        
            ScoreManager.Instance.ResetScore();
        });
        
        lvl3Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl3");
            UIManager.Show<HUDView>();
        
            ScoreManager.Instance.ResetScore();
        });
    }
}
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenuView : AView
{
    [SerializeField] private Button lvl1Button;
    [SerializeField] private Button lvl2Button;
    [SerializeField] private Button lvl3Button;
    [SerializeField] private Button backButton;
    public override void Initialize()
    {
        backButton.onClick.AddListener(() =>
        {
            DoHide();
            UIManager.ShowLast();
        });
        
        lvl1Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl1");
            UIManager.Show<HUDView>();
        });
        
        lvl2Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl2");
            UIManager.Show<HUDView>();
        });
        
        lvl3Button.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl3");
            UIManager.Show<HUDView>();
        });
    }
}
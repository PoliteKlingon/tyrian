using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : AView
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button startButton;
    public override void Initialize()
    {
        settingsButton.onClick.AddListener(() =>
            UIManager.Show<SettingsMenuView>());
        startButton.onClick.AddListener(() =>
        {
            DoHide();
            SceneManager.LoadScene("lvl1");
        });
    }
}

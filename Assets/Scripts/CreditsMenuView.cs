using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CreditsMenuView : AView
{
    [SerializeField] private Button backButton;
    public override void Initialize()
    {
        backButton.onClick.AddListener(() =>
        {
            DoHide();
            UIManager.ShowLast();
        });
    }
}

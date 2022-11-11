using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HUDView : AView
{
    [SerializeField] private TMP_Text scoreField;
    public override void Initialize()
    {
        
    }

    private void Update()
    {
        scoreField.text = ScoreManager.Instance.CurrentScore().ToString();
    }
}

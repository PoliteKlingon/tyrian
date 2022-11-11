using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {
        get; private set;

    }

    private float _score = 0;
    private float _distanceFlown = 0;

    void Awake()
    {
        // Check, if we do not have any instance yet.
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
            // Initialize references to other scripts.
            //InitializeReferences();
        } else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        _distanceFlown = 0;
    }

    private void Update()
    {
        if (_distanceFlown > 0.1f)
        {
            _distanceFlown -= 0.1f;
            AddScore(1);
        }
        else
        {
            _distanceFlown += Time.deltaTime;
        }
    }

    public void ResetScore()
    {
        _score = 0;
        _distanceFlown = 0;
    }

    public void AddScore(float score)
    {
        _score += score;
    }

    public float CurrentScore()
    {
        return _score;
    }
}

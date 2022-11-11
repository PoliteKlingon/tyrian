using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private static bool _paused = false;
    private void Awake()
    {
        // Check, if we do not have any instance yet.    
        if (Instance == null)
        {
            // 'this' is the first instance created => save it.
            Instance = this;
            
            // We want to keep the UI always present
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Destroy 'this' object as there exist another instance
            Destroy(this.gameObject);
        }
    }

    public static void PauseGame()
    {
        _paused = true;
        Time.timeScale = 0;
        
        UIManager.Show<PauseMenuView>(hideCurrent:false);
    }

    public static void ResumeGame()
    {
        _paused = false;
        Time.timeScale = 1;
        UIManager.ShowLast();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game Manager" 
            && Input.GetKeyUp(KeyCode.Escape) 
            && !ReferenceEquals(UIManager.GetCurrentViewType(), typeof(DeathMenuView)))
        {
            if (_paused)
            {
                if (ReferenceEquals(UIManager.GetCurrentViewType(), typeof(PauseMenuView)))
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }
}

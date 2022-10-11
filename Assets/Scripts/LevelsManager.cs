using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this); //nezmizi v jinym levelu
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game Manager")
        {
            SceneManager.LoadScene("lvl1");
        }
        // switch levels if N is pressed (released actually)
        if (Input.GetKeyUp(KeyCode.N))
        {
            int active = SceneManager.GetActiveScene().buildIndex;
            if (active + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogWarning("No more levels");
            }
            else
            {
                SceneManager.LoadScene("lvl" + (active + 1).ToString());                
            }
        }
    }
}

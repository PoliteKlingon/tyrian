using System.Collections;
using System.Collections.Generic;
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
        // switch levels if N is pressed (released actually)
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("lvl2");
        }
    }
}

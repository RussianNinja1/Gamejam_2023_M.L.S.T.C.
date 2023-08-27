using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] float gameOverLength = 4;
    [SerializeField] string sceneName = "MainMenu";

    float gameOverCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameOverCounter += Time.deltaTime;
        if (gameOverCounter > gameOverLength)
        {
            gameOverLength *= 1000;
            SceneManager.LoadScene(sceneName);
        }
    }
}

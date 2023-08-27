using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    public static float MaxHealth = 100;
    public static float speed = 5;
    public static string nextSceneName = "Level_1";

    private void Awake()
    {
        // Deletes duplicate managers
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Keeps manager persistent through levels
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }
}

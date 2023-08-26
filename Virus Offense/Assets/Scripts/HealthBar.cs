using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        // Hides bar when its value is either 0 or the max HP value. Reveals it otherwise.
        if (healthSlider.value == 0 || healthSlider.value == healthSlider.maxValue)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    //Sets the variables to their max value at start.
    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }

    //used to update the UI to the player's current health value
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}

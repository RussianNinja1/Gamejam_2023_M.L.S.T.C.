using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject healthSliderVisual;
    Slider healthSlider;

    private void Start()
    {
        healthSlider = healthSliderVisual.GetComponent<Slider>();
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

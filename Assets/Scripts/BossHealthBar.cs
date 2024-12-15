using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float health) // Tehd‰‰n public funktio jotta healthbarin arvo voi muuttua toista scripti‰ k‰ytt‰m‰ll‰
    {
        slider.maxValue = health;
        slider.value = health;

    }

    public void SetHealth(float health)
    {
        slider.value = health;

    }

    public void SetBarActive()
    {
        gameObject.SetActive(false);
    }

}

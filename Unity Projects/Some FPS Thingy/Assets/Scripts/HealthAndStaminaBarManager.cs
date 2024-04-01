using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndStaminaBarManager : MonoBehaviour
{
    [Header("Healthbar")]
    public Image HealthbarForeground;
    public Image StaminabarForeground;

    // Function to deal with healthbar
    public void UpdateHealthbar(float maxVal, float curVal)
    {
        HealthbarForeground.fillAmount = curVal / maxVal;
    }

    // Function to deal with healthbar
    public void UpdateStaminabar(float maxVal, float curVal)
    {
        StaminabarForeground.fillAmount = curVal / maxVal;
    }
}

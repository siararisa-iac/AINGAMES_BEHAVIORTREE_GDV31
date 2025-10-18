using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField]
    private Image hungerFill;
    [SerializeField]
    private Text hungerText;

    private void UpdateHunger(float current, float max)
    {
        hungerFill.fillAmount = current / max;
        hungerText.text = $"{current} / {max}";
    }

    private void Start()
    {
        PlayerHunger.OnHungerUpdate += UpdateHunger;
    }

    private void OnDestroy()
    {
        PlayerHunger.OnHungerUpdate -= UpdateHunger;
    }
}

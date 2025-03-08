using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text horseStaminaText;
    [SerializeField] private TMP_Text PlayerLocationText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text weatherText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void UpdateHealthUI(int health)
    {
        healthText.text = "" + health;
    }

    public void UpdateHorseStaminaUI(int stamina)
    {
        horseStaminaText.text = "" + stamina;
    }
    public void UpdateMoneyUI(int money)
    {
        horseStaminaText.text = "" + money;
    }

    public void UpdateTimeUI(int time)
    {
        timeText.text = time + "h";
    }
    public void UpdateWeatherUI(bool weather)
    {
        if (weather)
            weatherText.text = "Storm";
        else
            weatherText.text = "Clear";
    }

    public void UpdatePlayerLocationUI(Vector2 playerLocation)
    {
        //PlayerLocationText.text = "Location: (" + playerLocation.x + ", " + playerLocation.y + ")";
    }
}
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
        healthText.text = "Health: " + health;
    }

    public void UpdateTimeUI(int time)
    {
        timeText.text = "Time: " + time;
    }

    public void UpdateHorseStaminaUI(int stamina)
    {
        horseStaminaText.text = "HorseStamina: " + stamina;
    }

    public void UpdatePlayerLocationUI(Vector2 playerLocation)
    {
        //PlayerLocationText.text = "Location: (" + playerLocation.x + ", " + playerLocation.y + ")";
    }
}
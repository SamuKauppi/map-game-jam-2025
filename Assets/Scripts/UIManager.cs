using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    [SerializeField] private Text healthText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text horseStaminaText;
    [SerializeField] private Text PlayerLocationText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void UpdateHealthUI(int health)
    {
        //healthText.text = "Health: " + health;
    }

    public void UpdateTimeUI(int time)
    {
        //timeText.text = "Time: " + time;
    }

    public void UpdateHorseStaminaUI(int stamina)
    {
        //horseStaminaText.text = "HorseStamina: " + stamina;
    }

    public void UpdatePlayerLocationUI(Vector2 playerLocation)
    {
        //PlayerLocationText.text = "Location: (" + playerLocation.x + ", " + playerLocation.y + ")";
    }
}
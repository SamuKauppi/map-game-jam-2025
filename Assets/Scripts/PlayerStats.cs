using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats Instance { get; private set; }
    [SerializeField] private int health = 100;
    [SerializeField] private int time = 10;
    [SerializeField] private int horseStamina = 10;
    [SerializeField] private Vector2 PlayerLocation;





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

    private void DoDamage(int damage)
    {
        health -= damage;
        UiManager.Instance.UpdateHealthUI(health);
    }

    private void PlayerMove(Vector2 newLocation)
    {
        PlayerLocation = newLocation;
        UiManager.Instance.UpdatePlayerLocationUI(PlayerLocation);
    }

    private void TakeTime(int TakenTime)
    {
        time -= TakenTime;
        UiManager.Instance.UpdateTimeUI(time);
    }

    private void HorseTired(int HorseStaminaMinus)
    {
        horseStamina -= HorseStaminaMinus;
        UiManager.Instance.UpdateHorseStaminaUI(horseStamina);
    }

}
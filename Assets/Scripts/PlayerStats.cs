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
        health += damage;
    }

    private void PlayerMove()
    {

    }

    private void TakeTime(int TakenTime)
    {
        time += TakenTime;
    }

    private void HorseTired(int HorseStaminaMinus)
    {
        horseStamina += HorseStaminaMinus;
    }

}

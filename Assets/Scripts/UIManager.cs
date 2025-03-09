using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text horseStaminaText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text weatherText;

    private int hp = 0;
    private int stm = 0;
    private int money = 0;
    private int time = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private IEnumerator ReduceOverTime(TMP_Text targetText, int startAmount, int endAmount, string text = "")
    {
        float t = 0f;
        if (startAmount == 0)
        {
            t = 2.9f;
        }

        while (t < 3f)
        {
            targetText.text = Mathf.RoundToInt(Mathf.Lerp(startAmount, endAmount, t / 3f)).ToString() + text;
            t += Time.deltaTime;
            Debug.Log(targetText.text);
            yield return null;
        }
    }

    public void UpdateHealthUI(int health)
    {
        if (health < 0)
            health = 0;

        StartCoroutine(ReduceOverTime(healthText, hp, health));
        hp = health;
    }

    public void UpdateHorseStaminaUI(int stamina)
    {
        if (stamina < 0)
            stamina = 0;


        StartCoroutine(ReduceOverTime(horseStaminaText, stm, stamina));
        stm = stamina;
    }

    public void UpdateMoneyUI(int money)
    {
        if (money < 0) money = 0;

        StartCoroutine(ReduceOverTime(moneyText, this.money, money));
        this.money = money;
    }

    public void UpdateTimeUI(int time)
    {
        if (time < 0) time = 0;

        StartCoroutine(ReduceOverTime(timeText, this.time, time, " h"));
        this.time = time;
    }
    public void UpdateWeatherUI(bool weather)
    {
        if (weather)
            weatherText.text = "Storm";
        else
            weatherText.text = "Clear";
    }
}
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public bool IsPausedForEvent { get; set; } = false;

    [SerializeField] private int health = 100;
    [SerializeField] private int time = 10;
    [SerializeField] private int horseStamina = 100;
    [SerializeField] private int money = 300;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private bool weather = false;
    [SerializeField] private float staminaDifficultyScale = 5f;
    [SerializeField] private float timeDifficultyScale = 0.1f;

    public int Health => health;
    public int GameTime => time;
    public int HorseStamina => horseStamina;
    public int Money => money;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UiManager.Instance.UpdateHealthUI(health);
        UiManager.Instance.UpdateHorseStaminaUI(horseStamina);
        UiManager.Instance.UpdateMoneyUI(money);
        UiManager.Instance.UpdateTimeUI(time);
        UiManager.Instance.UpdateWeatherUI(weather);
    }

    private IEnumerator MoveBetweenPoints(Transform[] points, RouteType routeType)
    {
        int numSegments = points.Length - 1;
        int halfPoint = numSegments / 2;
        // Start at the first point
        transform.position = points[0].position;
        int currentSegment = 0;

        // Try to activate event once
        bool eventWasActivated = false;

        while (currentSegment < numSegments)
        {
            yield return new WaitWhile(() => IsPausedForEvent);

            Vector3 target = points[currentSegment + 1].position;
            // MoveTowards moves by a fixed distance per frame, ensuring constant speed.
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            // When we reach the target, move to the next segment.
            if (transform.position == target)
            {
                currentSegment++;
            }

            yield return null;

            // Check if event happens
            if (!eventWasActivated && currentSegment >= halfPoint)
            {
                // Try to activate event
                IsPausedForEvent = EventManager.Instance.TriggerEvent(routeType);
                eventWasActivated = true;
                Debug.Log("Trigger " + IsPausedForEvent);
            }
        }

        // Ensure the transform ends exactly at the last point.
        transform.position = points[^1].position;
        RouteManager.Instance.CompletedMovement();
    }


    public void DoDamage(int damage)
    {
        health -= damage;
        UiManager.Instance.UpdateHealthUI(health);
        Debug.Log("Health lost: " + damage);
        if (health <= 0)
        {
            EventManager.Instance.ShowEndGame("hp");
        }
    }

    public void HorseTired(int HorseStaminaMinus)
    {
        horseStamina -= HorseStaminaMinus;
        UiManager.Instance.UpdateHorseStaminaUI(horseStamina);
        Debug.Log("Stamina used: " + HorseStaminaMinus);
    }

    public void LoseMoney(int amount)
    {
        money -= amount;
        UiManager.Instance.UpdateMoneyUI(money);
        Debug.Log("Lost money: " + amount);
    }

    public void TakeTime(int TakenTime)
    {
        time -= TakenTime;
        UiManager.Instance.UpdateTimeUI(time);
        Debug.Log("Time taken: " + TakenTime);
        if (time <= 0)
        {
            EventManager.Instance.ShowEndGame("time");
        }
    }

    public void SetPlayerPos(Vector2 newLocation)
    {
        transform.position = newLocation;
    }
    public void PlayerMove(Transform[] moveRoute, RouteType type)
    {
        MovingStaminaAndTimeDecrease(moveRoute[0], moveRoute[moveRoute.Length - 1], type);
        StartCoroutine(MoveBetweenPoints(moveRoute, type));
    }

    private void MovingStaminaAndTimeDecrease(Transform startPos, Transform endPos, RouteType type) 
    { 
        float distance = Vector2.Distance(startPos.position, endPos.position);

        float stamina = distance * RouteMultiplier(type) * staminaDifficultyScale;
        float time = distance * RouteMultiplier(type) * timeDifficultyScale;

        TakeTime(Mathf.RoundToInt(time));
        HorseTired(Mathf.RoundToInt(stamina));
    }

    private float RouteMultiplier(RouteType type)
    {
        switch (type)
        {
            case RouteType.Road: return 1f;
            case RouteType.Offroad: return 2f;
            case RouteType.Boat: return 0.75f;
            case RouteType.Ship: return 0.5f;
            default: return 1f;
        }
    }

}
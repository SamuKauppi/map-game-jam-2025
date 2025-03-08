using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public bool IsPausedForEvent { get; set; } = false;

    [SerializeField] private int health = 100;
    [SerializeField] private int time = 10;
    [SerializeField] private int horseStamina = 10;
    [SerializeField] private int money = 100;
    [SerializeField] private float moveSpeed = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
                //IsPausedForEvent = EventManager.Instance.TriggerEvent();
                eventWasActivated = true;
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
    }

    public void SetPlayerPos(Vector2 newLocation)
    {
        transform.position = newLocation;
        UiManager.Instance.UpdatePlayerLocationUI(transform.position);
    }
    public void PlayerMove(Transform[] moveRoute, RouteType type)
    {
        StartCoroutine(MoveBetweenPoints(moveRoute, type));
    }

    public void TakeTime(int TakenTime)
    {
        time -= TakenTime;
        UiManager.Instance.UpdateTimeUI(time);
    }

    public void HorseTired(int HorseStaminaMinus)
    {
        horseStamina -= HorseStaminaMinus;
        UiManager.Instance.UpdateHorseStaminaUI(horseStamina);
    }

    public void LoseMoney(int amount)
    {
        money -= amount;
    }

}
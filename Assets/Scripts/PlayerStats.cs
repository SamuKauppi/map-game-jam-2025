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

    private IEnumerator MoveBetweenPoints(Transform[] points, EventType type)
    {
        int numSegments = points.Length - 1;
        // Start at the first point
        transform.position = points[0].position;
        int currentSegment = 0;

        while (currentSegment < numSegments)
        {
            Vector3 target = points[currentSegment + 1].position;
            // MoveTowards moves by a fixed distance per frame, ensuring constant speed.
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            // When we reach the target, move to the next segment.
            if (transform.position == target)
            {
                currentSegment++;
            }

            yield return null;
        }

        // Ensure the transform ends exactly at the last point.
        transform.position = points[points.Length - 1].position;
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
    public void PlayerMove(Transform[] moveRoute)
    {
        EventType type = EventType.FallingTree;
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
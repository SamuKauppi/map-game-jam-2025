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

    private IEnumerator MoveBetweenPoints(Transform[] points, float moveTime)
    {
        // Total number of segments between points
        int numSegments = points.Length - 1;
        float t = 0f;

        while (t < moveTime)
        {
            t += Time.deltaTime;
            // Normalize the time (0 to 1) over the whole moveTime
            float normalizedTime = Mathf.Clamp01(t / moveTime);

            // Determine our overall progress along the segments
            // Multiply by numSegments to convert normalized time into a segment-based value.
            float progress = normalizedTime * numSegments;
            // Determine which segment we're currently traversing
            int currentSegment = Mathf.Min(Mathf.FloorToInt(progress), numSegments - 1);
            // Calculate the local interpolation value within the current segment
            float segmentT = progress - currentSegment;

            // Lerp between the current point and the next point
            transform.position = Vector3.Lerp(points[currentSegment].position, points[currentSegment + 1].position, segmentT);

            yield return null;
        }

        // Ensure the transform ends exactly at the last point
        transform.position = points[^1].position;

        // Completed movement
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
    public void PlayerMove(Transform[] moveRoute, float transitionTime)
    {
        StartCoroutine(MoveBetweenPoints(moveRoute, transitionTime));
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private RoutePoint[] endPoints;
    [SerializeField] private Transform questPointer;

    private RoutePoint currentTarget;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChooseNewDestination();
    }

    public void ChooseNewDestination()
    {
        currentTarget = endPoints[Random.Range(0, endPoints.Length)];
        questPointer.transform.position = currentTarget.transform.position;
    }

    public void ReachedDestination(RoutePoint destination)
    {
        if (currentTarget == destination)
        {
            Debug.Log("Game complete");
            ChooseNewDestination();
        }
    }
}

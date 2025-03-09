using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static RouteManager Instance { get; private set; }

    public RoutePoint CurrentPoint { get { return currentPoint; } }

    [SerializeField] private RoutePoint[] routePoints;
    [SerializeField] private Route[] routes;

    [SerializeField] private Event[] stopEvents;

    private RoutePoint currentPoint;
    private Route currentRoute;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentPoint = routePoints[0];

        PlayerStats.Instance.SetPlayerPos(currentPoint.transform.position);
        ShowCurrentRoads();
    }

    public void ShowCurrentRoads()
    {
        foreach (Route route in routes)
        {
            route.gameObject.SetActive(route.HasTargetPoint(currentPoint));
        }

        foreach (RoutePoint point in routePoints)
        {
            point.gameObject.SetActive(point == currentPoint);
        }
    }

    public void MoveThroughRoute(RoutePoint targetPoint, Route targetRoute, Transform[] movePath)
    {
        // Update currentPoint
        currentPoint = targetPoint;
        currentRoute = targetRoute;

        // Move Player
        PlayerStats.Instance.PlayerMove(movePath, targetRoute.RouteType);

        // Show only current route
        foreach (Route route in routes)
        {
            if (route == currentRoute)
            {
                route.gameObject.SetActive(true);
                route.IsClickable = false;
                route.ChangeAlpha(1f);
            }
            else
            {
                route.gameObject.SetActive(false);
            }
        }
    }

    public void CompletedMovement()
    {
        currentRoute.IsClickable = true;
        currentRoute.ChangeAlpha(0.5f);
        ShowCurrentRoads();
        GameManager.Instance.ReachedDestination(currentPoint);

        if (!StormManager.Instance.isStorm)
        {
            UiManager.Instance.UpdateWeatherUI(false, Mathf.RoundToInt(StormManager.Instance.leftOfStorm));
        }
        else
        {
            UiManager.Instance.UpdateWeatherUI(true, Mathf.RoundToInt(StormManager.Instance.leftOfStorm));
        }

        if (currentPoint.IsAStop)
        {
            EventManager.Instance.ActivateEvent(stopEvents[Random.Range(0, stopEvents.Length)]);
        }

        if (PlayerStats.Instance.IsPlayerDead())
        {
            SceneLoader.Instance.LoadScene(SceneType.Lose);
        }
    }
}

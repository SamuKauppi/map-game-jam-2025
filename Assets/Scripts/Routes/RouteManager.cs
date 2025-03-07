using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static RouteManager Instance { get; private set; }

    public RoutePoint CurrentPoint { get { return currentPoint; } }

    [SerializeField] private RoutePoint[] routePoints;
    [SerializeField] private Route[] routes;

    private RoutePoint currentPoint;

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

    public void MoveThroughRoute(RoutePoint targetPoint)
    {
        currentPoint = targetPoint;
        // Move Player
        ShowCurrentRoads();
    }
}

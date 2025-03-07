using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [SerializeField] private Route[] routes;
    [SerializeField] private Transform currentPoint;

    [SerializeField] private Transform[] testPositions;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentPoint = testPositions[Random.Range(0, testPositions.Length)];
            ShowCurrentRoads();
        }
    }
    public void ShowCurrentRoads()
    {
        foreach (Route route in routes)
        {
            route.gameObject.SetActive(currentPoint.position == route.StartPoint.position);
        }
    }


}

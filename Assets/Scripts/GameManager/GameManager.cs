using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private RoutePoint[] endPoints;
    [SerializeField] private Transform questPointer;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject destinationCanvas;
    [SerializeField] private RoutePoint turku;

    private RoutePoint currentTarget;

    private int progress = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        destinationCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
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
            if (progress == 0)
            {
                destinationCanvas.SetActive(true);
                currentTarget = turku;
                questPointer.transform.position = currentTarget.transform.position;
            }
            if (progress == 1)
            {
                Debug.Log("Game won");
            }
            progress++;
        }
    }
}

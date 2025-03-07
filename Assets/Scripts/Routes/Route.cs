using UnityEngine;

public class Route : MonoBehaviour
{
    // Getters
    public RouteType RouteType { get { return type; } }

    [SerializeField] private RouteType type;
    [SerializeField] private HazardType hazardType;
    [SerializeField] private SpriteRenderer spriteRend;

    [SerializeField] private RoutePoint[] targetPoints;

    private void Start()
    {
        ChangeAlpha(0.5f);
    }

    private void OnMouseEnter()
    {
        ChangeAlpha(1f);
    }

    private void OnMouseExit()
    {
        ChangeAlpha(0.5f);
    }

    private void OnMouseDown()
    {
        RoutePoint point = null;
        foreach (RoutePoint p in targetPoints)
        {
            if (RouteManager.Instance.CurrentPoint != p)
            {
                point = p;
                break;
            }
        }

        if (point != null)
            RouteManager.Instance.MoveThroughRoute(point);
    }

    public void ChangeAlpha(float a)
    {
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, a);
    }

    public bool HasTargetPoint(RoutePoint currentPoint)
    {
        foreach (RoutePoint p in targetPoints)
        {
            if (p == currentPoint) return true;
        }

        return false;
    }
}

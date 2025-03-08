using Unity.VisualScripting;
using UnityEngine;

public class Route : MonoBehaviour
{
    // Getters
    public RouteType RouteType { get { return type; } }
    public bool IsClickable { get; set; } = true;

    [SerializeField] private RouteType type;
    [SerializeField] private SpriteRenderer spriteRend;

    [SerializeField] private RoutePoint startPoint;
    [SerializeField] private RoutePoint endPoint;

    private Transform[] playerMovePath;

    private void Start()
    {
        Transform[] movePath = new Transform[transform.childCount + 2];
        movePath[0] = startPoint.transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            movePath[i + 1] = transform.GetChild(i).transform;
        }

        movePath[^1] = endPoint.transform;

        playerMovePath = movePath;

        ChangeAlpha(0.75f);
    }

    private void OnMouseEnter()
    {
        if (!IsClickable) return;

        ChangeAlpha(1f);
    }

    private void OnMouseExit()
    {
        if (!IsClickable) return;

        ChangeAlpha(0.75f);
    }

    private void OnMouseDown()
    {
        if (!IsClickable) return;

        Transform[] movePath = new Transform[playerMovePath.Length];
        RoutePoint targetPoint;

        if (RouteManager.Instance.CurrentPoint != startPoint)
        {
            targetPoint = startPoint;

            // Copy in reverse order
            for (int i = playerMovePath.Length - 1; i >= 0; i--)
            {
                movePath[playerMovePath.Length - 1 - i] = playerMovePath[i];
            }
        }
        else
        {
            targetPoint = endPoint;

            // Copy in original order
            for (int i = 0; i < playerMovePath.Length; i++)
            {
                movePath[i] = playerMovePath[i];
            }
        }

        if (targetPoint != null)
            RouteManager.Instance.MoveThroughRoute(targetPoint, this, movePath);
    }


    public void ChangeAlpha(float a)
    {
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, a);
    }

    public bool HasTargetPoint(RoutePoint currentPoint)
    {
        if (startPoint == currentPoint || endPoint == currentPoint) return true;
        return false;
    }
}

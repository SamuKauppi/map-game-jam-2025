using UnityEngine;

public class Route : MonoBehaviour
{
    // Getters
    public RouteType RouteType { get { return type; } }
    public Transform StartPoint {  get { return startingPoint; } }
    public Transform EndPoint { get { return endPoint; } }


    [SerializeField] private RouteType type;
    [SerializeField] private HazardType hazardType;
    [SerializeField] private SpriteRenderer spriteRend;

    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform endPoint;

    private void Start()
    {
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0.5f);
    }

    private void OnMouseEnter()
    {
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 1f);
    }

    private void OnMouseExit()
    {
        spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatGainVisual : MonoBehaviour
{
    public static StatGainVisual Instance { get; private set; }

    [SerializeField] private RectTransform middlePos;
    [SerializeField] private RectTransform statPos;
    [SerializeField] private TMP_Text gainTextObject;
    private static readonly Dictionary<string, int> activeAnimations = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gainTextObject.gameObject.SetActive(false);
    }

    public void StartStatChangeAnimation(int amount, string type)
    {
        if (gainTextObject == null) return;

        // Check and limit simultaneous animations per type
        if (!activeAnimations.ContainsKey(type))
            activeAnimations[type] = 0;

        if (activeAnimations[type] > 0) return; // Prevent overlapping animations of same type

        // Create a new copy
        TMP_Text newTextObj = Instantiate(gainTextObject, statPos.position, Quaternion.identity, transform);
        newTextObj.gameObject.SetActive(true);

        // Set color based on type
        switch (type)
        {
            case "Stamina":
                newTextObj.color = Color.green;
                break;
            case "Time":
                newTextObj.color = Color.black;
                break;
            case "Health":
                newTextObj.color = Color.red;
                break;
            case "Money":
                newTextObj.color = Color.yellow;
                break;
            default:
                break;
        }

        amount *= -1;
        newTextObj.text = amount > 0 ? "+" + amount + " " + type : amount + " " + type;

        // Start animation
        StartCoroutine(AnimateTextObj(newTextObj, type));
    }

    private IEnumerator AnimateTextObj(TMP_Text obj, string type)
    {
        activeAnimations[type]++; // Track active coroutine
        obj.rectTransform.position = middlePos.position;

        yield return new WaitForSecondsRealtime(1.5f * (activeAnimations.Count));

        float t = 0;
        float transitionTime = 1f;
        while (t < transitionTime)
        {
            t += Time.deltaTime / transitionTime;
            obj.rectTransform.position = Vector3.Lerp(middlePos.position, statPos.position, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        // Clean up
        Destroy(obj.gameObject);
        activeAnimations[type]--; // Decrease count after animation ends
    }
}

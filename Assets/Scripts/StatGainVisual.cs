using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.TimeZoneInfo;

public class StatGainVisual : MonoBehaviour
{
    public static StatGainVisual Instance { get; private set; }

    [SerializeField] private RectTransform middlePos;
    [SerializeField] private RectTransform statPos;
    [SerializeField] private TMP_Text gainTextObject;

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

        gainTextObject.text = "";
        gainTextObject.gameObject.SetActive(true);

        switch (type)
        {
            case "Stamina":
                gainTextObject.color = Color.green;
                break;
            case "Time":
                gainTextObject.color = Color.black;
                break;
            case "Health":
                gainTextObject.color = Color.red;
                break;
            case "Money":
                gainTextObject.color = Color.yellow;
                break;
            default:
                break;
        }

        amount *= -1;
        gainTextObject.text = amount > 0 ? "+" + amount + " " + type : amount + " " + type;

        StartCoroutine(AnimateTextObj(gainTextObject));
    }

    private IEnumerator AnimateTextObj(TMP_Text obj)
    {
        obj.rectTransform.position = middlePos.position;

        yield return new WaitForSecondsRealtime(2f);

        float t = 0;
        float transitionTime = 1f;
        while (t < transitionTime)
        {
            t += Time.deltaTime / transitionTime;
            obj.rectTransform.position = Vector3.Lerp(middlePos.position, statPos.position, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);
        obj.gameObject.SetActive(false);
    }
}

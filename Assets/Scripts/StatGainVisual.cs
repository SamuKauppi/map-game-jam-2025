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
    [SerializeField] private TMP_Text gainText;

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
        gainText.gameObject.SetActive(false);
    }

    public void StartStatChangeAnimation(int amount, string type)
    {
        TMP_Text newGainText = Instantiate(gainText);
        newGainText.text = "";
        amount *= -1;
        if(amount > 0)
        {
            newGainText.text = "+" + amount + type;
        }
        else
        {
            newGainText.text += amount + type;
        }    

        newGainText.gameObject.SetActive(true);
        StartCoroutine(AnimateTextObj(newGainText));
    }

    private IEnumerator AnimateTextObj(TMP_Text obj)
    {
        obj.rectTransform.position = middlePos.position;

        yield return new WaitForSecondsRealtime(1f);

        float t = 0;
        float transitionTime = 1f;
        while (t < transitionTime)
        {
            t += Time.deltaTime / transitionTime;
            obj.rectTransform.position = Vector3.Lerp(middlePos.position, statPos.position, t);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        Destroy(obj.gameObject);
    }
}

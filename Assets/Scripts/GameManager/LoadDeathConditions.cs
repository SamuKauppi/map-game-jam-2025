using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadDeathConditions : MonoBehaviour
{
    [SerializeField] private Image deathImage;
    [SerializeField] private TMP_Text deathText;

    private void Start()
    {
        if (SceneLoader.Instance.EndSprite != null)
        {
            deathImage.sprite = SceneLoader.Instance.EndSprite;
        }
        if (SceneLoader.Instance.EndText != "")
        {
            deathText.text = SceneLoader.Instance.EndText;
        }
    }
}

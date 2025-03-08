using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [SerializeField] private Event[] events;

    [SerializeField] private GameObject popUpWindow;
    [SerializeField] private Image popup_img;
    [SerializeField] private TMP_Text popup_txt;
    [SerializeField] private TMP_Text choice1_txt;
    [SerializeField] private TMP_Text choice2_txt;

    [SerializeField] private GameObject closeWindow;
    [SerializeField] private Image close_img;
    [SerializeField] private TMP_Text close_txt;

    private Event currentEvent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        popUpWindow.SetActive(false);
        closeWindow.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerEvent(EventType.Moose);
        }
    }

    private void HandleStatChange(StatType type, int amount)
    {
        switch (type)
        {
            case StatType.Health:
                PlayerStats.Instance.DoDamage(amount);
                break;
            case StatType.Stamina:
                PlayerStats.Instance.HorseTired(amount);
                break;
            case StatType.Money:
                PlayerStats.Instance.LoseMoney(amount);
                break;
            case StatType.Time:
                PlayerStats.Instance.TakeTime(amount);
                break;
            default:
                break;
        }
    }

    public void TriggerEvent(EventType type)
    {

        foreach (Event e in events)
        {
            if (e.type == type)
            {
                currentEvent = e;
                break;
            }
        }

        if (currentEvent == null)
            return;

        popUpWindow.SetActive(true);
        closeWindow.SetActive(false);

        popup_img.sprite = currentEvent.eventSprite;
        popup_txt.text = currentEvent.eventText;
        choice1_txt.text = currentEvent.option1Txt;
        choice2_txt.text = currentEvent.option2Txt;

        close_img.sprite = currentEvent.eventSprite;
    }

    public void CloseEvent(int choice)
    {
        popUpWindow.SetActive(false);
        closeWindow.SetActive(true);
        close_txt.text = choice == 0 ? currentEvent.option1EndTxt : currentEvent.option2EndTxt;
        HandleStatChange(choice == 0 ? currentEvent.option1 : currentEvent.option2, choice == 0 ? currentEvent.amount1 : currentEvent.amount2);
    }

    public void StopEvent()
    {
        popUpWindow.SetActive(false);
        closeWindow.SetActive(false);
    }
}

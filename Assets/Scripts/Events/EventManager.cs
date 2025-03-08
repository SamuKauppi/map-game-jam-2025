using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [SerializeField] private int eventChance = 20;

    [SerializeField] private Event[] landEvents;
    [SerializeField] private Event[] waterEvents;

    [SerializeField] private GameObject popUpWindow;
    [SerializeField] private GameObject popUpOptions;
    [SerializeField] private GameObject quitScreen;

    [SerializeField] private Image popup_img;
    [SerializeField] private TMP_Text popup_txt;
    [SerializeField] private TMP_Text choice1_txt;
    [SerializeField] private TMP_Text choice2_txt;

    [SerializeField] private GameObject closeOptions;
    [SerializeField] private Sprite gameOverSprite;

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
        closeOptions.SetActive(false);
        popUpOptions.SetActive(false);
        quitScreen.SetActive(false);
    }

    private void Update()
    {
        
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

    public bool TriggerEvent(RouteType rType)
    {
        int roll = Random.Range(0, 101);

        // Return if the event wont happen.
        if (roll > eventChance)
            return false;

        if ((rType == RouteType.Road || rType == RouteType.Offroad) && landEvents.Length != 0)
            currentEvent = landEvents[roll % landEvents.Length];

        else if ((rType == RouteType.Boat || rType == RouteType.Ship) && waterEvents.Length != 0)
            currentEvent = waterEvents[roll % waterEvents.Length];


        if (currentEvent == null)
            return false;

        popUpWindow.SetActive(true);
        popUpOptions.SetActive(true);
        closeOptions.SetActive(false);

        popup_img.sprite = currentEvent.eventSprite;
        popup_txt.text = currentEvent.eventText;
        choice1_txt.text = currentEvent.option1Txt;
        choice2_txt.text = currentEvent.option2Txt;

        return true;
    }

    public void CloseEvent(int choice)
    {
        popUpOptions.SetActive(false);
        closeOptions.SetActive(true);
        popup_txt.text = choice == 0 ? currentEvent.option1EndTxt : currentEvent.option2EndTxt;
        popup_img.sprite = currentEvent.eventSprite;
        HandleStatChange(choice == 0 ? currentEvent.option1 : currentEvent.option2, choice == 0 ? currentEvent.amount1 : currentEvent.amount2);
        currentEvent = null;
    }

    public void StopEvent()
    {
        popUpWindow.SetActive(false);
        popUpOptions.SetActive(false);
        closeOptions.SetActive(false);
        PlayerStats.Instance.IsPausedForEvent = false;
    }

    public void ShowEndGame()
    {
        if(currentEvent == null) return;
        popUpWindow.SetActive(true);
        closeOptions.SetActive(false);
        popUpOptions.SetActive(false);
        quitScreen.SetActive(true);
        popup_txt.text = currentEvent.gameOverText;
        popup_img.sprite = gameOverSprite;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

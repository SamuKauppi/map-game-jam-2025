using UnityEngine;

[System.Serializable]
public class Event
{
    public EventType type;

    public Sprite eventSprite;

    [TextArea(3, 15)]
    public string eventText;

    public StatType option1;
    public int amount1;
    [TextArea(1, 10)]
    public string option1Txt;
    [TextArea(3, 15)]
    public string option1EndTxt;

    public StatType option2;
    public int amount2;
    [TextArea(1, 10)]
    public string option2Txt;
    [TextArea(3, 15)]
    public string option2EndTxt;
}

public enum StatType
{
    Health,
    Stamina,
    Money,
    Time
}

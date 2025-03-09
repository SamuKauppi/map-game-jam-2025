using UnityEngine;

[System.Serializable]
public class Event
{
    public EventType type;

    public Sprite eventSprite;

    [TextArea(3, 15)]
    public string eventText;

    public StatChange[] option1Effects;
    [TextArea(1, 10)]
    public string option1Txt;
    [TextArea(3, 15)]
    public string option1EndTxt;

    public StatChange[] option2Effects;
    [TextArea(1, 10)]
    public string option2Txt;
    [TextArea(3, 15)]
    public string option2EndTxt;

    [TextArea(3, 10)]
    public string gameOverText;

    public Sprite gameOverSprite;
}

public enum StatType
{
    Health,
    Stamina,
    Money,
    Time
}

[System.Serializable]
public class StatChange
{
    public StatType type;
    public int amount;
}

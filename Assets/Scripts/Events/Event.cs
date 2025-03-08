using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

[System.Serializable]
public class Event
{
    public EventType type;

    public Sprite eventSprite;

    [TextArea(3, 15)]
    public string eventText;

    //public StatType option1;
    //public int amount1;
    [TextArea(1, 10)]
    public string option1Txt;
    [TextArea(3, 15)]
    public string option1EndTxt;
    public List<StatChange> option1Changes;

    //public StatType option2;
    //public int amount2;
    [TextArea(1, 10)]
    public string option2Txt;
    [TextArea(3, 15)]
    public string option2EndTxt;
    public List<StatChange> option2Changes;

    [TextArea(3, 10)]
    public string gameOverText;
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



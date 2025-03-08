using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormManager : MonoBehaviour
{
    public static StormManager Instance { get; private set; }

    private int stormDuration = 3;
    //private int timebeforeStorm = 1;
    public bool isStorm = false;
    public float stormMultiplier = 2f;
    public int stormDamage = 10;
    private int stormChance = 20;
    private float stormStart;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckStorm()
    {
        if (isStorm)
        {
            ContinueStorm();
        }
        else
        {
            int roll = Random.Range(0, 101);
            if (roll < stormChance)
                StartStorm();
        }
    }

    private void StartStorm()
    {
        isStorm = true;
        stormStart = PlayerStats.Instance.GameTime;
        UiManager.Instance.UpdateWeatherUI(true);
        Debug.Log("Storm started");
    }

    private void ContinueStorm()
    {
        float timeFromStart = stormStart - PlayerStats.Instance.GameTime;
        Debug.Log("Storm has been raging on for: " + timeFromStart + " hours.");
        if ((timeFromStart) > stormDuration )
            EndStorm();
    }

    private void EndStorm()
    {
        isStorm = false;
        UiManager.Instance.UpdateWeatherUI(false);
        Debug.Log("Storm Ended");
    }
}

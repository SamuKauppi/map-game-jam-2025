using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormManager : MonoBehaviour
{
    public static StormManager Instance { get; private set; }

    private int stormDuration = 3;
    private int timebeforeStorm = 1;
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
            {
                Debug.Log("Storm started");
                StartStorm();
            }
        }
    }

    private void StartStorm()
    {
        isStorm = true;
        stormStart = PlayerStats.Instance.GameTime;
        UiManager.Instance.UpdateWeatherUI(true);
    }

    private void ContinueStorm()
    {
        float time = PlayerStats.Instance.GameTime;
        float timeFromStart = stormStart - time;
        if ((timeFromStart) > stormDuration )
            EndStorm();
    }

    private void EndStorm()
    {
        isStorm = false;
        UiManager.Instance.UpdateWeatherUI(false);
    }
}

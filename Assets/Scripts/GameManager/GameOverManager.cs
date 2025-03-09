using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //private void Start()
    //{
    //    StartCoroutine(CheckGameOver());
    //}

    //private IEnumerator CheckGameOver()
    //{
    //    while (true)
    //    {
    //        if (PlayerStats.Instance != null)
    //        {
    //            if (PlayerStats.Instance.Health <= 0 ||
    //                PlayerStats.Instance.GameTime <= 0)
    //            {
    //                TriggerGameOver();
    //                yield break;
    //            }
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Pause the game
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader Instance { get; private set; }
    public string EndText { get; private set; }
    public Sprite EndSprite { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadScene(SceneType type)
    {
        var index = type switch
        {
            SceneType.Menu => 0,
            SceneType.Game => 1,
            SceneType.Win => 2,
            SceneType.Lose => 3,
            _ => 0,
        };
        SceneManager.LoadScene(index);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void UpdateDeathConditions(string text, Sprite sprite)
    {
        EndText = text;
        EndSprite = sprite;
    }
}

public enum SceneType
{
    Menu,
    Game,
    Win,
    Lose
}

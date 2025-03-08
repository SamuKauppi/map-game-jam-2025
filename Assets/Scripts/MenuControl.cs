using UnityEngine;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private SceneType sceneToLoad;
    public void StartGame()
    {
        SceneLoader.Instance.LoadScene(sceneToLoad);
    }
}

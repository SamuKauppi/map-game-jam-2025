using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private int sceneId;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneId);
    }
}

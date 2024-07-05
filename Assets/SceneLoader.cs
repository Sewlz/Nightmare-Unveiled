using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetString("NextScene", sceneName);
        SceneManager.LoadScene("LoadingScreen");
    }
}

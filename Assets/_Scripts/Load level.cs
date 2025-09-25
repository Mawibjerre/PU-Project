using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public void LoadScene(int levelID)
    {
        SceneManager.LoadScene(levelID);
    }

    public void Quitgame()
    {
        Application.Quit();
    }

}

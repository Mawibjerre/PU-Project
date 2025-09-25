using UnityEngine;

public class Pause : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Escape;
    public GameObject pauseMenu;
    bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            PauseSystem();
        }
    }

    public void PauseSystem()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            if(pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
            Time.timeScale = 0f;
        }
        else
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
            Time.timeScale = 1f;
        }
    }
}

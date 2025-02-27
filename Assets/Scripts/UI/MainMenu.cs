using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Area 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

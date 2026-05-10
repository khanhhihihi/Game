using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public void PlayGame()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("character");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

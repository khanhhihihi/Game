using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSelect : MonoBehaviour
{
    public void SelectNgoc()
    {
        PlayerPrefs.SetString("SelectedCharacter", "Ngoc");
        SceneManager.LoadScene("Level 1");
    }

    public void SelectTien()
    {
        PlayerPrefs.SetString("SelectedCharacter", "Tien");
        SceneManager.LoadScene("Level 1");
    }
}

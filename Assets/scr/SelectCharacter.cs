using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public GameObject [] characters; // Ngoc, Tien
    public TextMeshProUGUI playerNameText;
    private int currentIndex = 0;

    void Start()
    {
        if (characters != null && characters.Length > 0)
        {
            ShowCharacter(currentIndex);
        }
        else
        {
            Debug.LogError("Characters array is empty! Gán prefab Ngoc/Tien trong Inspector.");
        }
    }

    void ShowCharacter(int index)
    {
        if (characters == null || characters.Length == 0) return;
        if (index < 0 || index >= characters.Length) return;

        // Tắt tất cả
        foreach (GameObject c in characters)
        {
            if (c != null) c.SetActive(false);
        }

        // Bật nhân vật hiện tại
        if (characters[index] != null)
        {
            characters[index].SetActive(true);
            if (playerNameText != null)
            {
                playerNameText.text = characters[index].name;
            }
        }
    }

    public void NextCharacter()
    {
        if (characters == null || characters.Length == 0) return;
        currentIndex = (currentIndex + 1) % characters.Length;
        ShowCharacter(currentIndex);
    }

    public void PreviousCharacter()
    {
        if (characters == null || characters.Length == 0) return;
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        ShowCharacter(currentIndex);
    }

    public void Select()
    {
        if (characters != null && characters.Length > 0)
        {
            PlayerPrefs.SetString("SelectedCharacter", characters[currentIndex].name);
        }
    }
    public void OpenLevel(int levelId)
    {
        // Lấy nhân vật đã chọn trước đó
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter", "DefaultCharacter");

        // Tạo tên scene theo level
        string levelName = "Level" + levelId;

        // Load scene đúng level
        SceneManager.LoadScene(levelName);
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);
        }
        else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);

            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            // Gọi hàm mở khóa level mới
            UnlockNewLevel();

            if (currentIndex + 1 < totalScenes)
            {
                // còn scene → load scene tiếp
                SceneManager.LoadScene(currentIndex + 1);
            }
            else
            {
                // hết scene → WIN
                gameManager.GameWin();
            }
        }
        void UnlockNewLevel()
        {
            // Nếu level hiện tại (scene đang chơi) có chỉ số lớn hơn chỉ số đã đạt được trước đó
            if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("ReachedIndex"))
            {
                // Cập nhật "ReachedIndex" thành scene hiện tại + 1
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);

                // Tăng số level đã mở khóa thêm 1 (mặc định bắt đầu từ 1 nếu chưa có)
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);

                // Lưu lại dữ liệu vào bộ nhớ
                PlayerPrefs.Save();
            }
        }


    }
}
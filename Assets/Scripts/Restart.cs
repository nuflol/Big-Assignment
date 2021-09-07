using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour {
    public Text gameOverText;
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameController.Score = 0;
        GameController.Health = 4;
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = true;
    }
}

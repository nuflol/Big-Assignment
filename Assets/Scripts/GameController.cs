using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    private static int health = 4;
    private static int maxHealth = 4;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static int score = 0;
    

    public Text gameOverText;
    public Text healthText;
    public Text scoreText;
    public bool _isDead = false;
    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static int Score { get => score; set => score = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    
    void Update() {
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;

        if (health <= 0 && Time.timeScale > 0) 
            GameOver();
    }
    
    public static void DamagePlayer(int damage) {
        health -= damage;
    }

    public static void HealPlayer(int healAmmount) {
        Health = Mathf.Min(maxHealth, health + healAmmount);
    }

    private static void KillPlayer() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = false;
    }

    void GameOver() {
            Time.timeScale = 0;
            KillPlayer();
            gameOverText.GetComponent <Text>();
            gameOverText.enabled = true;
    }
}

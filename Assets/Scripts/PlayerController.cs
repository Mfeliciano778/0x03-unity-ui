using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController character;
    Vector3 moveVec;
    public float speed = 15f;
    int score;
    int health;
    public Text scoreText;
    public Text healthText;
    public Text winloseText;
    public Image winloseImg;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        score = 0;
        health = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        character.Move(moveVec * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (health == 0)
        {
            // Debug.Log("Game Over!");
            SetLoseText();
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        moveVec = new Vector3(direction.x, 0, direction.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            // disable game object
            other.gameObject.SetActive(false);
            score += 1;
            SetScoreText();
            // Debug.Log($"Score: {score}");
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();
            // Debug.Log($"Health: {health}");
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            SetWinText();
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void SetWinText()
    {
        winloseImg.gameObject.SetActive(true);
        winloseImg.color = Color.green;
        winloseText.color = Color.black;
        winloseText.text = "You Win!";
    }

    void SetLoseText()
    {
        winloseImg.gameObject.SetActive(true);
        winloseImg.color = Color.red;
        winloseText.color = Color.white;
        winloseText.text = "Game Over!";
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

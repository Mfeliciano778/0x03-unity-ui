using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController character;
    Vector3 moveVec;
    public float speed = 15f;
    private int score;
    public int health;
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
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            Debug.Log($"Score: {score}");
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            health -= 1;
            Debug.Log($"Health: {health}");
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
    }
}

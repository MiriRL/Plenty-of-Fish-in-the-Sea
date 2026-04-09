using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    Vector2 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Obstacle"))
        {
            transform.position = startPos;
            
        }
    }
}

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
            //Replace with ending the minigame, either ending screen or sending back to the date
            
        }
    }
}

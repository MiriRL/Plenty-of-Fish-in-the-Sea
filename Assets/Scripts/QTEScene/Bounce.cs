using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody.linearVelocityX == 0)
        {
            rigidbody.linearVelocity = new Vector2(Random.Range(1,3), rigidbody.linearVelocityY);
        }

        else if(rigidbody.linearVelocityY == 0)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocityX,Random.Range(1,3));
        }
    }

    void OnColisionEnter2D(Collision2D collision)
    {
        
    }
}

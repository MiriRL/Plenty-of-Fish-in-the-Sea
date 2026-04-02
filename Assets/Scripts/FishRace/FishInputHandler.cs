using UnityEngine;

public class FishInputHandler : MonoBehaviour
{

    FishRacingFish fishRacingFish;

    void Awake()
    {
        fishRacingFish = GetComponent<FishRacingFish>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        fishRacingFish.SetInputVector(inputVector);
    }

    void OnColisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            
        }
    }
}

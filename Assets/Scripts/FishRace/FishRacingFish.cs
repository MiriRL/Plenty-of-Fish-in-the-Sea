using UnityEngine;
using System.Collections;
using UnityEditor.UI;
using Unity.VisualScripting;

public class FishRacingFish : MonoBehaviour
{

    Rigidbody2D fish;
    public float acceleration = 30.0f;
    public float turning = 3.5f;
    public float drift =  0.5f;

    float accelInput = 0;
    float steerInput = 0;

    float carAngle = 0;

    public float movespeed;

    void Awake()
    {
        fish = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ApplyAccel();

        //KillVelocity();

        ApplySteer();
    }

    void ApplyAccel()
    {
        if(accelInput == 0)
        {
            fish.linearDamping = Mathf.Lerp(fish.linearDamping, 3.0f, Time.fixedDeltaTime *3);
        }
        else
        {
            fish.linearDamping = 0;
        }
        Vector2 fishForce = transform.up*accelInput*acceleration;
        fish.AddForce(fishForce, ForceMode2D.Force);

        
    }

    void ApplySteer()
    {
        carAngle -= steerInput * turning;
        fish.MoveRotation(carAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steerInput = inputVector.x;
        accelInput = inputVector.y;
    }

    void KillVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(fish.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.up * Vector2.Dot(fish.linearVelocity, transform.right);

        fish.linearVelocity = forwardVelocity + rightVelocity * drift;
    }

    
}

using UnityEngine;
using System.Collections;

public class FishRacingFish : MonoBehaviour
{

    Rigidbody2D fish;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;

        transform.Translate(movement * (10 * Time.deltaTime));
        
    }
}

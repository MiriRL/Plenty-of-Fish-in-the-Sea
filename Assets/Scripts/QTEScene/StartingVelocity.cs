using UnityEngine;

public class StartingVelocity : MonoBehaviour
{

    public Vector2 initialVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(this.transform.localPosition);
    }
}

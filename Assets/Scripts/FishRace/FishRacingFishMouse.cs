using UnityEngine;
using UnityEngine.InputSystem;

public class FishRacingFishMouse : MonoBehaviour
{

    public float moveSpeed = 10;
    public float smoothTime = 0.3f;
    public float mindistance = 1f;
    public float maxTurnSpeed=90;
    float angle;

    float currentVelocityAngle;
    Vector2 currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePositionRel = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = mousePositionRel - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
        //transform.eulerAngles = new Vector3 (0, 0, angle);

        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocityAngle, smoothTime, maxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);
        

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition += ((Vector2)transform.position - mousePosition).normalized * mindistance;
        transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, moveSpeed);
    }
}

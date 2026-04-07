using UnityEngine;

public class ObstaclesScroll : MonoBehaviour
{
    public float speed;
    private Vector2 targetPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var pos = this.transform.position;
        targetPos = new Vector2(pos.x, pos.y - 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
    }
}

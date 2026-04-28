using System.Collections;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitForDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(3);
        Object.Destroy(this.gameObject);
    }
}

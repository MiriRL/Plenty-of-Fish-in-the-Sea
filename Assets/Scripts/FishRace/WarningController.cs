using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    public float interval;
    private float count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        InvokeRepeating("DisappearanceLogic", 0, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisappearanceLogic() 
{
     if(this.gameObject.activeSelf) 
     {
         this.gameObject.SetActive(false);
         count+=1;
     }
     else
     {
         this.gameObject.SetActive(true);
         count+=1;
     }
     if(count > 5)
        {
           Object.Destroy(this.gameObject); 
        }
}
}

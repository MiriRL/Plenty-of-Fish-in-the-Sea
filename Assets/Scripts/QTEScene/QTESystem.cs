using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    
    public GameObject DisplayBox;
    public GameObject PassFailBox;
    public int KeyGen;
    public int WaitForKey;
    public int CorrectKey;
    public int Countdown;

    void Update()
    {
        
        if(WaitForKey == 0)
        {
            KeyGen = Random.Range(1,4);
            Countdown = 1;
            StartCoroutine(CountingDown());

            if(KeyGen == 1)
            {
                WaitForKey = 1;
                DisplayBox.GetComponent<TextMeshProUGUI>().text = "E";
            }

            if(KeyGen == 2)
            {
                WaitForKey = 1;
                DisplayBox.GetComponent<TextMeshProUGUI>().text = "R";
            }

            if(KeyGen == 3)
            {
                WaitForKey = 1;
                DisplayBox.GetComponent<TextMeshProUGUI>().text = "T";
            }
        }
        
        if(KeyGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("QTE E"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if(KeyGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("QTE R"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if(KeyGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("QTE T"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        KeyGen = 4;
        if(CorrectKey == 1)
        {
            Countdown = 2;
            PassFailBox.GetComponent<TextMeshProUGUI>().text  = "Good Job!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassFailBox.GetComponent<TextMeshProUGUI>().text  = "";
            DisplayBox.GetComponent<TextMeshProUGUI>().text  = "";
            yield return new WaitForSeconds(1.5f);

            WaitForKey = 0;
            Countdown = 1;
        }

        if(CorrectKey == 2)
        {
            Countdown = 2;
            PassFailBox.GetComponent<TextMeshProUGUI>().text  = "Wrong Key!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassFailBox.GetComponent<TextMeshProUGUI>().text  = "";
            DisplayBox.GetComponent<TextMeshProUGUI>().text  = "";
            yield return new WaitForSeconds(1.5f);

            WaitForKey = 0;
            Countdown = 1;
        }
    }

        IEnumerator CountingDown()
        {
            yield return new WaitForSeconds(3.5f);
            if(Countdown == 1)
            {
                KeyGen = 4;
                Countdown = 2;
                PassFailBox.GetComponent<TextMeshProUGUI>().text = "Too Late!";

                CorrectKey = 0;        
                yield return new WaitForSeconds(1.5f);
                PassFailBox.GetComponent<TextMeshProUGUI>().text  = "";
                DisplayBox.GetComponent<TextMeshProUGUI>().text  = "";  

                WaitForKey = 0;
                Countdown = 1;
            }
        
        
        }


}

// using UnityEngine;

// public class FlipCard : MonoBehaviour
// {

//     //https://github.com/pronaypeddiraju/Memory-Game/blob/master/Memory%20Game/Assets/Scripts/gameManager.cs
//     // ^ maybe use as needed

//     //initialize card values
//     string[] level1 = {"Favorite Color", "Blue", "Best Friend", "Salmon", "Favorite App", "Fishbook", "Favorite Activity", "Swimming"};
//     string[] level2additions = {"Mom's Name", "Debra", "Favorite Drink", "Water", "Dream Job", "Doctor", "Hometown", "The Little Pond"};
//     //card numbers is equal to 8 * level
//     int level; 
//     string[] cardArray = new string[level * 8];
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         if (level == 1)
//         {
//             cardArray = level1;
//         }
//         // on level 2, increase card amount to 16
//         else if (level == 2)
//         {
//             for (int i = 0; i < 8; i++)
//             {
//                 cardArray[i] = level1[i];
//                 cardArray[i + 8] = level2additions[i];
//             }     
//         }
//         //randomly order cards
//         System.Array.Sort(cardArray, RandomSort);
//         // establish matches
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//      // this function just returns a number in the range -1 to +1
//     // and is used by Array.Sort to 'shuffle' the array
//     //sourced from https://discussions.unity.com/t/random-shuffle-array/443149/2
//     int RandomSort(string a, string b)
//     {
//         return Random.Range(-1, 2);

//     }

//     void handleFlip()
//     {
//         // when button is pressed,
//         // flip card,
//         // if two cards are now flipped,
//             // if checkMatch
//                 // checkWin
//             // else
//                 //turn back over
//     }

//     // bool checkMatch(string a, string b)
//     // {
//     //     // in some data structure, store a -> b
//     //     //check if a and b are correlated
//     // }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    public Text nameDisplayText;// Assign via Inspector
    //public Text displayScore;

    void Start()
    {
        nameDisplayText.text = "Score: " + Player.playerName + ": " + Player.playerScore; // Display the stored name
    }
}

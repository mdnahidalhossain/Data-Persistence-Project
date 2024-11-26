using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public InputField inputField; // Assign via Inspector

    public void SaveNameAndSwitchScene()
    {
        Player.playerName = inputField.text; // Store the entered name
        SceneManager.LoadScene("main"); // Replace with your new scene's name
    }
}

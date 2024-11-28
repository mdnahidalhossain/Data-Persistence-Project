using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainManager;
using static MenuManager;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Text highestScore;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    private void Start()
    {
        LoadScore();
    }


    public void LoadScore()
    {
        string filePath = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

            highestScore.text = $"Highest Score: {scoreData.bestPlayerName}: {scoreData.bestScore}";
        }
        else
        {
            highestScore.text = "Highest Score: No Data";
        }


    }

}

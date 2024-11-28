using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainManager;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public int playerScore;
    //public Text playerName;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public Text playerNameAndScore;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        Player.playerScore = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        playerNameAndScore.text = $"Score: {Player.playerName}: {m_Points}";
        GameOverText.SetActive(true);

        SaveScore();
        
    }

    [System.Serializable]
    public class ScoreData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveScore()
    {
        ScoreData scoreData = new ScoreData();

        // Update the best score if the current score is higher
        if (m_Points > scoreData.bestScore)
        {
            scoreData.bestPlayerName = Player.playerName;
            scoreData.bestScore = m_Points;

            string json = JsonUtility.ToJson(scoreData, true); // Convert to JSON string
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // Save to file
        }
    }

    //public void LoadScore()
    //{
    //    string filePath = Application.persistentDataPath + "/savefile.json";

    //    if (File.Exists(filePath))
    //    {
    //        string json = File.ReadAllText(filePath);
    //        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);
    //    }
    //    else
    //    {
    //        ScoreData scoreData = new ScoreData();
    //    }
    //}


}

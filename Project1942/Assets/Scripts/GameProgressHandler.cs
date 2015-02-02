using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameProgressHandler : MonoBehaviour
{

    float score = 0;
    Text scoreText;
    public  GameObject scoreObject;

    Text waveText;
    public  GameObject waveObject;
    int totalWaves = 0;
    int currentWave = 0;

    public GameObject shipSpawner;


    public static GameProgressHandler instance { get; private set; }
    
    void Awake()
    {
        instance = this;
    }


    public  int enemiesActive = 0;
    public GameObject winObject;
    public GameObject loseObject;


    bool lost = false;

    // Use this for initialization
	void Start ()
    {
        scoreText = scoreObject.GetComponent<Text>();
        AddScore(0);

        waveText = waveObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ((currentWave == totalWaves) && (enemiesActive <= 0) && lost == false)
        {
            winObject.SetActive(true);
        }

        if(lost == true && Input.GetKey(KeyCode.Space) )
        {
           Application.LoadLevel(Application.loadedLevelName);
        }

	}

  public void AddScore( float change)
    {
        score += change;
        score = Mathf.Max(0, score);
        scoreText.text = "Score: " + score;
    }

  public void StartLevel(int numOfWaves)
  {
      totalWaves = numOfWaves;
      currentWave = 0;
  }
  public void WaveFinished()
  {
      currentWave++;
      waveText.text = "Wave " + currentWave + " of " + totalWaves;      
  }

  public void LoseGame()
  {
      loseObject.SetActive(true);
      lost = true;
      shipSpawner.SetActive(false);

  }
}

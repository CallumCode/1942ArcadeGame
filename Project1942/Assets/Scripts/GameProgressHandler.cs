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


    public static GameProgressHandler instance { get; private set; }
    
    void Awake()
    {
        instance = this;
    }



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
	
	}

  public  void AddScore( float change)
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
}

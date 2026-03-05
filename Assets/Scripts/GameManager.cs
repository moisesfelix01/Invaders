using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreDisplay;
    public TextMeshProUGUI highScoreDisplay;
    public GameObject startingDisplay;
    public GameObject gameSprites;

    //player preference
    private const string HIGHSCORE = "High Score";
    private int highScore;

    private int playerScore = 0;
    
    private int enemyCount = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Grab high score from player preferences, default set to zero if nothing
        highScore = PlayerPrefs.GetInt(HIGHSCORE, 0);
        highScoreDisplay.text = "HIGH SCORE\n" + highScore.ToString("D4");
        //Short pause at the start of the game to display UI
        StartCoroutine(HaltedStart());
    }

    void OnEnable()
    {
        //Subscribe to player bullets
        PlayerBullet.targetHit += UpdatePlayerScore;
        PlayerBullet.targetHit += UpdateEnemyCount;
    }
    void OnDisable()
    {
        //Unsubscribe to player bullets
        PlayerBullet.targetHit -= UpdatePlayerScore;
        PlayerBullet.targetHit -= UpdateEnemyCount;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerScore(GameObject _object)
    {
        //Increase score based on target hit
        if (_object.CompareTag("Squid"))
        {
            playerScore += 30;
            Destroy(_object);
        }
        else if (_object.CompareTag("Bug"))
        {
            playerScore += 20;
            Destroy(_object);
        }
        else if (_object.CompareTag("Jellyfish"))
        {
            playerScore += 10;
            Destroy(_object);
        }
        else if (_object.CompareTag("UFO"))
        {
            int random = Random.Range(5,10);
            playerScore += random * 10;
            Destroy(_object);
        }
        else if (_object.CompareTag("Barricade"))
        {
            Destroy(_object);
        }

        //Update player score display
        string temp = "Score\n" + playerScore.ToString("D4");
        playerScoreDisplay.text = temp;

        //Update high score if player score is larger
        if (playerScore >= highScore)
        {
            //Save new score in player preferences
            PlayerPrefs.SetInt(HIGHSCORE, playerScore);
            PlayerPrefs.Save();
            //update high score UI
            highScoreDisplay.text = "HIGH SCORE\n" + playerScore.ToString("D4");
        }
    }

    void UpdateEnemyCount(GameObject _object)
    {
        //Win notification when all enemies are gone
        enemyCount--;
        if (enemyCount == 0)
        {
            Debug.Log("YOU WIN!");
        }
    }

    private IEnumerator HaltedStart()
    {
        //pause game and display starting UI
        Time.timeScale = 0f;
        gameSprites.SetActive(false);
        startingDisplay.SetActive(true);
        //wait for three seconds
        yield return new WaitForSecondsRealtime(3f);
        //unpause game and remove starting UI
        //load in sprites after halted start
        gameSprites.SetActive(true);
        startingDisplay.SetActive(false);
        Time.timeScale = 1f;

    }
}

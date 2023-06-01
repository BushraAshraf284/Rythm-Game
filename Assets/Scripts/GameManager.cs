using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Track[] tracks;
    public AudioSource source;
    [Header("Game Variables")]
    public int score;
    public float LifeTime;
    [Header("Settings Variables")]
    public float startTime = 2.0f;
    public int hitBlockScore = 10;
    public float missBlockLife = 0.1f;
    public float wrongBlockLife = 0.8f;
    public float lifeRegenrate = 0.3f;
    public float SwordHitVelocityThreshold = 0.5f;
    public VelocityTracker velocityTrackerA;
    public VelocityTracker velocityTrackerB;
    public float swordHitVelocityThreshold = 0.5f;
    [Header("Audio Variables")]
    public float volumeChangeDuration = 5f;

    private Track currentTrack;
    private int trackIndex;
    private float elapsedTime;
    private bool changingScene;
    // Singleton
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
       if(Instance!=null)
            Destroy(Instance);

        Instance = this;

    }
    #endregion

    private void Start()
    {
        elapsedTime = 0;
        changingScene = false;
        trackIndex = PlayerPrefs.GetInt("SongID");
        for(int i = 0; i< tracks.Length; i++)
        {
            tracks[i].gameObject.SetActive(false);
        }
        currentTrack = tracks[trackIndex];
        currentTrack.gameObject.SetActive(true);
        currentTrack.Init();
        
    }

    public void AddScore()
    {
        score += hitBlockScore;
        UIController.Instance.UpdatescoreText();
        
    }

    public void MissBlock()
    {
        LifeTime -= missBlockLife;
    }

    public void HitWrongBlock()
    {
        LifeTime -= wrongBlockLife;
    }

    // Returns whether the swords velocity is greater than the threshold depending on which sword is being used
    public bool isGreaterThanThreshold(Color color) 
    {
        return color == Color.COLORA ? velocityTrackerA.velocity.magnitude > 0.5f : velocityTrackerB.velocity.magnitude > 0.5f;
    }

    private void Update()
    {
        if(LifeTime>=0)
            LifeTime = Mathf.MoveTowards(LifeTime, 1.0f, lifeRegenrate * Time.deltaTime);       
        else
            LoseGame();

        UIController.Instance.UpdateLifeBar();
        if (changingScene)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(source.volume, 0, 0.01f);
        }
     
        

    }

    public void LoseGame()
    {
        PlayerPrefs.SetInt("Win", 0);
        EndGame();
    }

   

    public void WinGame()
    {
        PlayerPrefs.SetInt("Win", 1);
        EndGame();
    }

    public void EndGame()
    {
        changingScene = true;
        currentTrack.Stop = true;
        PlayerPrefs.SetInt("EndScene", 1);
        PlayerPrefs.SetInt("Score", score);        
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Home");
    }

}

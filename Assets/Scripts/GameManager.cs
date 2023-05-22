using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public int score;
    public float LifeTime;
    [Header("Settings Variables")]
    public float startTime = 2.0f;
    public int hitBlockScore = 10;
    public float missBlockLife = 0.1f;
    public float wrongBlockLife = 0.8f;
    public float lifeRegenrate = 0.1f;
    public float SwordHitVelocityThreshold = 0.5f;

    public VelocityTracker velocityTrackerA;
    public VelocityTracker velocityTrackerB;
    public float swordHitVelocityThreshold = 0.5f;

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

    }

    public void LoseGame()
    {
        SceneManager.LoadScene(0);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(0);
    }

}

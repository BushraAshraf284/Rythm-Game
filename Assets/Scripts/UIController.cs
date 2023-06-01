using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text ScoreText;
    public Image LifeTimeBar;
    public TMP_Text Message;
    // Singleton
    #region Singleton
    public static UIController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;

    }
    #endregion
    private void Start()
    {
        Message.gameObject.SetActive(false);
    }
    public void UpdatescoreText()
    {
        ScoreText.text = GameManager.Instance.score.ToString();
    }

    public void UpdateLifeBar()
    {
        LifeTimeBar.fillAmount = GameManager.Instance.LifeTime;
    }

    public void ShowMessage( bool isWin)
    {
        Message.gameObject.SetActive(true);
        Message.text = isWin ? "You Win!" : "You Lose!"; 
    }
    
}

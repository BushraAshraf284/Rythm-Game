using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text ScoreText;
    public Image LifeTimeBar;
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
    public void UpdatescoreText()
    {
        ScoreText.text = GameManager.Instance.score.ToString();
    }

    public void UpdateLifeBar()
    {
        LifeTimeBar.fillAmount = GameManager.Instance.LifeTime;
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    [Header("Ui References")]
    public TMP_Text Score;
    public TMP_Text GameStatus;
    public GameObject[] Highlight;

    public int SongIndex;
   
    void Start()
    {
        PlayerPrefs.SetInt("SongID", 0);
        LoadEndPanel();
    }

    public void LoadEndPanel()
    {
        if (PlayerPrefs.GetInt("EndScene") == 1)
        {
            GameStatus.text = PlayerPrefs.GetInt("Win") == 1 ? "You Win" : "You Lose!";
            Score.text = PlayerPrefs.GetInt("Score").ToString();
            ManagePanels((int)Panels.END);
            PlayerPrefs.SetInt("EndScene", 0);
        }
        else
        {
            ManagePanels((int)Panels.START);
        }    
    }
    public void SetIndex(int index)
    {
        for (int i = 0; i < Highlight.Length; i++)
        {
            Highlight[i].SetActive(false);
        }
        SongIndex = index;
        Highlight[index].SetActive(true);
    }

    public void SelectSong()
    {
        PlayerPrefs.SetInt("SongID", SongIndex);
        SceneManager.LoadScene("GamePlay");
    }


    /// <summary>
    /// Function to switch between panels
    /// </summary>
    /// <param name="panelIndex">Panel you want to switch to</param>
    public void ManagePanels(int panelIndex)
    {
        for(int i =0; i< panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        Debug.Log("Panel Index"+ panelIndex);
        panels[panelIndex].SetActive(true);

    }
}

public enum Panels
{
    START,
    SONGSELECTION,
    END
}

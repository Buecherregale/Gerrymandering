using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour {
    [SerializeField] private Button storyButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    public void Start() {
        storyButton.onClick.AddListener(StartStory);
        easyButton.onClick.AddListener(delegate { StartDifficulty(Difficulty.Easy); });
        mediumButton.onClick.AddListener(delegate { StartDifficulty(Difficulty.Medium); });
        hardButton.onClick.AddListener(delegate { StartDifficulty(Difficulty.Hard); });
    }

    public void StartStory() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// When a difficulty is selected, save it to player prefs and load the game scene
    /// </summary>
    /// <param name="difficulty"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void StartDifficulty(Difficulty difficulty ) {
        if (difficulty == Difficulty.Easy)
        {
            PlayerPrefs.SetString("difficulty", difficulty.ToString());
            PlayerPrefs.Save();
            SceneManager.LoadScene(0);
        }
        else {
            throw new NotImplementedException();
        }
        
    }
    
    [System.Serializable]
    public enum Difficulty {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
}

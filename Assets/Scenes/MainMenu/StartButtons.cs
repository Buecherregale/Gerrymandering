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
        //throw new NotImplementedException();
        SceneManager.LoadScene(0);
    }

    public void StartDifficulty(Difficulty difficulty ) {
        throw new NotImplementedException();
    }
    
    [System.Serializable]
    public enum Difficulty {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
}

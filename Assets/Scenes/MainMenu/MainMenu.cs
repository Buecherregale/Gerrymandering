using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   [SerializeField] private GameObject mainUI;
   [SerializeField] private GameObject levelUI;
   [SerializeField] private GameObject optionsUI;
   [SerializeField] private GameObject howUI;
   [SerializeField] private GameObject infoUI;
   
   public void LoadLevelUI() {
      mainUI.SetActive(false);
      levelUI.SetActive(true);
   }

   public void Quit() {
      Application.Quit();
   }

   public void BackToMainMenu() {
      mainUI.SetActive(true);
      optionsUI.SetActive(false);
      infoUI.SetActive(false);
      howUI.SetActive(false);
      levelUI.SetActive(false);
   }

   public void LoadOptions() {
      mainUI.SetActive(false);
      optionsUI.SetActive(true);
   }
   
   public void LoadInfo() {
      mainUI.SetActive(false);
      infoUI.SetActive(true);
   }
   
   public void LoadHowToPlay() {
      mainUI.SetActive(false);
      howUI.SetActive(true);
   }
}

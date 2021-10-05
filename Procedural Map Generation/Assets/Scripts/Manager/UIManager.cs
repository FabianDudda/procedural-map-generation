using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("UI References")]
    public GameObject SeedUI;
    public Text SeedTxt;            
       

    public void StartGame()
    {
        float seedInput = float.Parse(SeedTxt.text);        // Convert string to float
        MapManager.Instance.SetSeedAndStartGame(seedInput); // Call SetSeed() in MapManager
        SeedUI.SetActive(false);                            // Deactivates the UI for entering a seed
    }
}

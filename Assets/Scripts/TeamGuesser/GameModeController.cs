using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    ToggleGroup toggleGroup;
    GameManager gameManager;


    // Start is called before the first frame update
    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void onChangeMode(string mode)
    {
        gameManager.gamemode = mode;
    }
}

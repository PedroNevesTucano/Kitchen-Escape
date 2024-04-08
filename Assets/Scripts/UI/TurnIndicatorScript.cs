using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicatorScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] private TextMeshProUGUI turnIndicatorTXT;
    [SerializeField] public GameData gameManager;
    [SerializeField] public GameData.GameState previousState;
    [SerializeField] public int turnNumber;
    [SerializeField] public bool turnChanged;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState != previousState)
        {
            if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
            {
                turnNumber++;
            }
            previousState = gameManager.gameState;
        }

        turnIndicatorTXT.text = "Turn: " + turnNumber.ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift) && gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            gameManager.gameState = GameData.GameState.PlayerTurn;
        }
    }
}

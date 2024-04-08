using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharPlacementButtonScript;

public class EndPlacementScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Assigned Elements")]
    [SerializeField] private Button button;
    [SerializeField] public GameData gameManager;

    [Header("Verifications")]
    [SerializeField] public bool canEnd;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        button = GetComponent<Button>();
    }

    private void Update()
    {
        GameObject[] charPlacementButtons = GameObject.FindGameObjectsWithTag("CharPlacementButton");
        bool allPlaced = true;

        foreach (GameObject charPlacementButton in charPlacementButtons)
        {
            CharPlacementButtonScript charPlacementButtonScript = charPlacementButton.GetComponent<CharPlacementButtonScript>();

            if (charPlacementButtonScript != null && !charPlacementButtonScript.placed)
            {
                allPlaced = false;
                break;
            }
        }

        if (canEnd)
        {
            button.image.color = Color.white;
        }
        else if (!canEnd)
        {
            button.image.color = Color.gray;
        }

        canEnd = allPlaced;
    }


    public void OnButtonClick()
    {
        if (canEnd)
        {
            gameManager.gameState = GameData.GameState.Cutscene;
        }
    }
}

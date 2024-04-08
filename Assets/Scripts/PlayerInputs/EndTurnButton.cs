using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameData gameManager;
    [SerializeField] public playerInput playerInp;
    [SerializeField] private Button button;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    private void Update()
    {
        if (!playerInp.SelectedCharacterBool)
        {
            button.image.color = Color.white;
        }
        else
        {
            button.image.color = Color.gray;
        }
    }

    public void OnButtonClick()
    {
        if (!playerInp.SelectedCharacterBool)
        {
            gameManager.gameState = GameData.GameState.EnemyTurn;
        }
    }
}

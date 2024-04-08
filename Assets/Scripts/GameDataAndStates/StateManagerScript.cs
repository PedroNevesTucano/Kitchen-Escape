using UnityEngine;

public class StateManagerScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameObject CharPlacementStateCanvas;
    [SerializeField] public GameObject CutsceneStateCanvas;
    [SerializeField] public GameObject PlayerTurnStateCanvas;
    [SerializeField] public GameObject PlayerTurnAndEnemyTurnStateCanvas;
    [SerializeField] public GameData gameManager;
    [SerializeField] public playerInput playerInp;
    [SerializeField] public GameObject selectedCharOverlay;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        CharPlacementStateCanvas.SetActive(gameManager.gameState == GameData.GameState.CharacterPlacement);
        CutsceneStateCanvas.SetActive(gameManager.gameState == GameData.GameState.Cutscene);
        PlayerTurnStateCanvas.SetActive(gameManager.gameState == GameData.GameState.PlayerTurn);
        PlayerTurnAndEnemyTurnStateCanvas.SetActive(gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn);

        selectedCharOverlay.SetActive(playerInp.SelectedCharacterBool);
    }
}

using UnityEngine;

public class HealthBarOrientationScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameData gameManager;
    private Camera mainCamera;
    private GameObject[] childObjects;
    public GameObject playerAura;
    public GameObject indicatorsBackground;
    public GameObject canWalkIndicator;
    public GameObject canAttackIndicator;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            if (mainCamera != null)
            {
                SetChildObjectsActive(true);

                Vector3 directionToCamera = mainCamera.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(directionToCamera);
            }
        }
        else
        {
            SetChildObjectsActive(false);
        }
    }

    private void SetChildObjectsActive(bool active)
    {
        foreach (var child in childObjects)
        {
            child.SetActive(active);
        }
        playerAura.SetActive(active);
        indicatorsBackground.SetActive(active && gameManager.gameState == GameData.GameState.PlayerTurn);
        canWalkIndicator.SetActive(active && gameManager.gameState == GameData.GameState.PlayerTurn);
        canAttackIndicator.SetActive(active && gameManager.gameState == GameData.GameState.PlayerTurn);
    }
}

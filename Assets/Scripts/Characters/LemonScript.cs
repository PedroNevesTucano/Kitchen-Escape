using UnityEngine;

public class LemonScript : SharedCharacterAttributesScript
{
    [Header("Assigned Elements")]
    [SerializeField] public GameData gameManager;

    [Header("Verifications")]
    [SerializeField] private bool hasReset;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem dustParticleSystem = gameObject.transform.Find("Dust Particle System").GetComponent<ParticleSystem>();
        dustParticleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCurrentNode();

        if (gameManager.gameState == GameData.GameState.PlayerTurn && !hasReset)
        {
            if (currentEffect != SharedCharacterAttributesScript.Effect.Prone)
            {
                ResetWalkingAndAttacking();
            }
            hasReset = true;
        }
        else if (gameManager.gameState != GameData.GameState.PlayerTurn)
        {
            hasReset = false;
        }

        if (health <= 0)
        {
            currentGridX = -1;
            currentGridY = -1;
            Destroy(gameObject);
        }
    }

    void MoveToCurrentNode()
    {
        string nodeName = "Node_" + currentGridX + "_" + currentGridY;
        GameObject node = GameObject.Find(nodeName);
        if (node != null)
        {
            GoToSelectedNode(node.transform.position.x, node.transform.position.z);
        }
        else
        {
            Debug.LogError("Node " + nodeName + " not found!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SharedCharacterAttributesScript;

public class SelectedCharTypeScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] private RawImage attackTypeIMG;
    public SelectedCharImageUIScript selectedCharIMG;
    public Texture2D CircleType;
    public Texture2D SquareType;
    public Texture2D TriangleType;
    public Texture2D DiamondType;
    public Texture2D NullType;
    public playerInput playerImp;

    [SerializeField] public GameData gameManager;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn) && selectedCharIMG.charSelectedOnBoard != null && !playerImp.SelectedCharacterBool)
        {
            switch (selectedCharIMG.charSelectedOnBoard.attackType)
            {
                case AttackType.Circle:
                    attackTypeIMG.texture = CircleType;
                    break;
                case AttackType.Square:
                    attackTypeIMG.texture = SquareType;
                    break;
                case AttackType.Triangle:
                    attackTypeIMG.texture = TriangleType;
                    break;
                case AttackType.Diamond:
                    attackTypeIMG.texture = DiamondType;
                    break;
                default:
                    attackTypeIMG.texture = NullType;
                    break;
            }
        }
        if (selectedCharIMG.canDisapear)
        {
            attackTypeIMG.texture = NullType;
        }
    }
}

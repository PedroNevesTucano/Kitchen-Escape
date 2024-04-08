using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCharImageUIScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    public Texture2D LemonIMG;
    public Texture2D PumpkinIMG;
    public Texture2D BananaIMG;
    public Texture2D OnionIMG;
    public Texture2D NullIMG;
    public RawImage IMG;
    public MouseGuide mouseGuide;
    public GameObject charPlacement;
    public SelectedCharStatsUI charStatsUI;
    [SerializeField] public GameData gameManager;
    public playerInput playerImp;

    [Header("Verifications")]
    [SerializeField] public bool nullChar;
    [SerializeField] public bool canDisapear;

    [SerializeField] public SharedCharacterAttributesScript charSelectedOnBoard;


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
        if (IMG.texture == NullIMG)
        {
            nullChar = true;
        }
        else
        {
            nullChar = false;
        }

        if ((charSelectedOnBoard == null || !mouseGuide.gameObject.activeInHierarchy) && !playerImp.SelectedCharacterBool)
        {
            canDisapear = true;
        } 
        else
        {
            canDisapear = false;
        }

        charPlacement = playerImp.CharPlacement;
        charSelectedOnBoard = playerImp.characterOnNode;

        if (charSelectedOnBoard != null && charPlacement == null && !playerImp.SelectedCharacterBool)
        {
            switch (charSelectedOnBoard.name)
            {
                case "Lemon(Clone)":
                    IMG.texture = LemonIMG;
                    break;
                case "Pumpkin(Clone)":
                    IMG.texture = PumpkinIMG;
                    break;
                case "Banana(Clone)":
                    IMG.texture = BananaIMG;
                    break;
                case "Onion(Clone)":
                    IMG.texture = OnionIMG;
                    break;
                default:
                    break;
            }
        }

        if (gameManager.gameState == GameData.GameState.CharacterPlacement)
        {
            if (charPlacement != null)
            {
                switch (charPlacement.name)
                {
                    case "PlacementLemon(Clone)":
                        IMG.texture = LemonIMG;
                        break;
                    case "PlacementPumpkin(Clone)":
                        IMG.texture = PumpkinIMG;
                        break;
                    case "PlacementBanana(Clone)":
                        IMG.texture = BananaIMG;
                        break;
                    case "PlacementOnion(Clone)":
                        IMG.texture = OnionIMG;
                        break;
                    default:
                        // Handle other cases if necessary
                        break;
                }
            }
            else if ((charPlacement == null && charSelectedOnBoard == null) || !mouseGuide.gameObject.activeInHierarchy)
            {
                IMG.texture = NullIMG;
            }
        } else if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            if ((charSelectedOnBoard == null || !mouseGuide.gameObject.activeInHierarchy) && !playerImp.SelectedCharacterBool)
            {
                IMG.texture = NullIMG;
            }
        }

        if (charSelectedOnBoard != null && !playerImp.SelectedCharacterBool)
        {
            charStatsUI.SetSelectedCharacter(charSelectedOnBoard);
        }
    }
}

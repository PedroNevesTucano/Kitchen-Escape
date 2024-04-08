using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SharedCharacterAttributesScript;

public class SelectedCharStatsUI : MonoBehaviour
{
    [Header("Verifications")]
    [SerializeField] private bool activated;
    [SerializeField] private float speed;

    [Header("Assigned Elements")]
    [SerializeField] private TextMeshProUGUI LableText;

    [SerializeField] private TextMeshProUGUI HealthValueText;
    [SerializeField] private TextMeshProUGUI MoveSpeedValueText;
    [SerializeField] private RawImage RangeValueImg;
    [SerializeField] private TextMeshProUGUI AttackValueText;
    [SerializeField] private TextMeshProUGUI AttackSpeedValueText;
    [SerializeField] private TextMeshProUGUI DefenseValueText;
    [SerializeField] private RawImage AttackTypeValueImg;
    [SerializeField] public GameData gameManager;
    public SelectedCharImageUIScript selectedCharIMG;
    public playerInput playerImp;
    public Texture2D CircleType;
    public Texture2D SquareType;
    public Texture2D TriangleType;
    public Texture2D DiamondType;
    public Texture2D NullType;

    public Texture2D MeleeIcon;
    public Texture2D RangedIcon;

    [SerializeField] public GameObject Lemon;
    [SerializeField] public GameObject Pumpkin;
    [SerializeField] public GameObject Banana;
    [SerializeField] public GameObject Onion;

    private RectTransform rectTransform;
    private SharedCharacterAttributesScript charAttributes;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activated = !activated;
        }

        if (gameManager.gameState == GameData.GameState.CharacterPlacement)
        {
            Vector2 targetPosition = activated ? new Vector2(rectTransform.anchoredPosition.x, 25f) : new Vector2(rectTransform.anchoredPosition.x, 225f);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * speed);
        } 
        else if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            Vector2 targetPosition = activated ? new Vector2(295f, -105f) : new Vector2(505f, -105f);
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * speed);
        }
        UpdateStatsUI();
    }

    void UpdateStatsUI()
    {
        if (gameManager.gameState == GameData.GameState.CharacterPlacement)
        {
            SharedCharacterAttributesScript attributesToDisplay = null;

            if (selectedCharIMG != null && selectedCharIMG.charPlacement != null)
            {
                switch (selectedCharIMG.charPlacement.name)
                {
                    case "PlacementLemon(Clone)":
                        attributesToDisplay = Lemon.GetComponent<SharedCharacterAttributesScript>();
                        break;
                    case "PlacementPumpkin(Clone)":
                        attributesToDisplay = Pumpkin.GetComponent<SharedCharacterAttributesScript>();
                        break;
                    case "PlacementBanana(Clone)":
                        attributesToDisplay = Banana.GetComponent<SharedCharacterAttributesScript>();
                        break;
                    case "PlacementOnion(Clone)":
                        attributesToDisplay = Onion.GetComponent<SharedCharacterAttributesScript>();
                        break;
                }
            }
            else if (charAttributes != null && selectedCharIMG.charSelectedOnBoard != null && selectedCharIMG.charPlacement == null)
            {
                attributesToDisplay = charAttributes;
            }


            if (attributesToDisplay != null)
            {
                LableText.text = attributesToDisplay.name.Replace("(Clone)", "") + "'s Stats";
                HealthValueText.text = attributesToDisplay.health.ToString();
                //RangeValueText.text = attributesToDisplay.range.ToString();
                AttackValueText.text = attributesToDisplay.attack.ToString();
                DefenseValueText.text = attributesToDisplay.defence.ToString();

                switch (attributesToDisplay.attackType)
                {
                    case AttackType.Circle:
                        AttackTypeValueImg.texture = CircleType;
                        break;
                    case AttackType.Square:
                        AttackTypeValueImg.texture = SquareType;
                        break;
                    case AttackType.Triangle:
                        AttackTypeValueImg.texture = TriangleType;
                        break;
                    case AttackType.Diamond:
                        AttackTypeValueImg.texture = DiamondType;
                        break;
                    default:
                        AttackTypeValueImg.texture = NullType;
                        break;
                }

                switch (attributesToDisplay.range)
                {
                    case AttackDistance.Ranged:
                        RangeValueImg.texture = RangedIcon;
                        break;
                    case AttackDistance.Melee:
                        RangeValueImg.texture = MeleeIcon;
                        break;
                    default:
                        RangeValueImg.texture = NullType;
                        break;
                }
            }

            if (selectedCharIMG.nullChar)
            {
                LableText.text = "Character's" + " Stats";
                HealthValueText.text = "*";
                //RangeValueText.text = "*";
                AttackValueText.text = "*";
                DefenseValueText.text = "*";
                AttackTypeValueImg.texture = NullType;
                RangeValueImg.texture = NullType;
            }
        } 
        else if (gameManager.gameState == GameData.GameState.PlayerTurn || gameManager.gameState == GameData.GameState.EnemyTurn)
        {
            if (selectedCharIMG.charSelectedOnBoard != null && !playerImp.SelectedCharacterBool)
            {
                LableText.text = selectedCharIMG.charSelectedOnBoard.name.Replace("(Clone)", "") + "'s Stats";

                HealthValueText.text = selectedCharIMG.charSelectedOnBoard.health.ToString();
                //RangeValueText.text = attributesToDisplay.range.ToString();
                AttackValueText.text = selectedCharIMG.charSelectedOnBoard.attack.ToString();
                DefenseValueText.text = selectedCharIMG.charSelectedOnBoard.defence.ToString();

                switch (selectedCharIMG.charSelectedOnBoard.range)
                {
                    case AttackDistance.Ranged:
                        RangeValueImg.texture = RangedIcon;
                        break;
                    case AttackDistance.Melee:
                        RangeValueImg.texture = MeleeIcon;
                        break;
                    default:
                        RangeValueImg.texture = NullType;
                        break;
                }
            }

            if (selectedCharIMG.canDisapear)
            {
                LableText.text = "Character's" + " Stats";
                HealthValueText.text = "*";
                //RangeValueText.text = "*";
                AttackValueText.text = "*";
                DefenseValueText.text = "*";
                RangeValueImg.texture = NullType;
            }
        }
    }

    // Method to set the selected character's attributes
    public void SetSelectedCharacter(SharedCharacterAttributesScript selectedCharacter)
    {
        charAttributes = selectedCharacter;
    }
}

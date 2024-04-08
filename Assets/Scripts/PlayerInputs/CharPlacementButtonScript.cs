using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharPlacementButtonScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameObject OnionPlacementPrefab;
    [SerializeField] public GameObject LemonPlacementPrefab;
    [SerializeField] public GameObject BananaPlacementPrefab;
    [SerializeField] public GameObject PumpkinPlacementPrefab;
    [SerializeField] public GameData gameManager;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;

    [Header("Verifications")]
    [SerializeField] public ButtonChar buttonChar;
    [SerializeField] public bool placed;
    [SerializeField] public bool placing;

    private GameObject instantiatedPrefab;

    public enum ButtonChar
    {
        Onion,
        Banana,
        Pumpkin,
        Lemon
    }

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        switch (buttonChar)
        {
            case ButtonChar.Onion:
                buttonText.text = "Onion";
                break;
            case ButtonChar.Lemon:
                buttonText.text = "Lemon";
                break;
            case ButtonChar.Banana:
                buttonText.text = "Banana";
                break;
            case ButtonChar.Pumpkin:
                buttonText.text = "Pumpkin";
                break;
            default:
                buttonText.text = "Unknown";
                break;
        }
    }

    private void Update()
    {
        if (instantiatedPrefab == null && GameObject.FindWithTag(buttonChar.ToString()) == null)
        {
            button.image.color = Color.white;
        }
        else if (instantiatedPrefab != null || GameObject.FindWithTag(buttonChar.ToString()) != null)
        {
            button.image.color = Color.gray;
        }

        if (GameObject.FindWithTag(buttonChar.ToString()) != null)
        {
            placed = true;
            placing = false;
        }
        else
        {
            placed = false;
        }

        if (instantiatedPrefab == null && placed == false)
        {
            placing = false;
        }
    }

    public void OnButtonClick()
    {
        if (instantiatedPrefab == null && GameObject.FindWithTag(buttonChar.ToString()) == null && CanPlace())
        {
            switch (buttonChar)
            {
                case ButtonChar.Onion:
                    instantiatedPrefab = Instantiate(OnionPlacementPrefab);
                    break;
                case ButtonChar.Banana:
                    instantiatedPrefab = Instantiate(BananaPlacementPrefab);
                    break;
                case ButtonChar.Lemon:
                    instantiatedPrefab = Instantiate(LemonPlacementPrefab);
                    break;
                case ButtonChar.Pumpkin:
                    instantiatedPrefab = Instantiate(PumpkinPlacementPrefab);
                    break;
            }
            placing = true;
        }
    }

    private bool CanPlace()
    {
        CharPlacementButtonScript[] allButtonScripts = FindObjectsOfType<CharPlacementButtonScript>();
        foreach (CharPlacementButtonScript buttonScript in allButtonScripts)
        {
            if (buttonScript != this && buttonScript.placing)
            {
                return false;
            }
        }
        return true;
    }
}

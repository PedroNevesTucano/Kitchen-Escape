using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public TextMeshProUGUI buttonText;
    public SelectedCharImageUIScript selectedCharIMG;
    [SerializeField] public playerInput playerInp;
    [SerializeField] private Button button;
    [SerializeField] AttackButton attackButton;

    [Header("Verifications")]
    [SerializeField] public bool usingAbility;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
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

        if (usingAbility)
        {
            buttonText.text = "Deactivate" + " Ability";
        }
        else
        {
            buttonText.text = "Activate" + " Ability";
        }
    }

    public void OnButtonClick()
    {
        if (!usingAbility && !playerInp.SelectedCharacterBool)
        {
            usingAbility = true;
            if (attackButton.attacking)
            {
                attackButton.attacking = false;
            }
        }
        else if (usingAbility && !playerInp.SelectedCharacterBool)
        {
            usingAbility = false;
        }


    }
}
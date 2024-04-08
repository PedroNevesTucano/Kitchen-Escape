using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public TextMeshProUGUI buttonText;
    public SelectedCharImageUIScript selectedCharIMG;
    [SerializeField] public playerInput playerInp;
    [SerializeField] private Button button;
    [SerializeField] AbilityButton abilityButton;

    [Header("Verifications")]
    [SerializeField] public bool attacking;

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

        if (attacking)
        {
            buttonText.text = "Deactivate" + " Attack";
        } else
        {
            buttonText.text = "Activate" + " Attack";
        }
    }

    public void OnButtonClick()
    {
        if (!attacking && !playerInp.SelectedCharacterBool) 
        {
            attacking = true;
            if (abilityButton.usingAbility)
            {
                abilityButton.usingAbility = false;
            }
        }
        else if (attacking && !playerInp.SelectedCharacterBool)
        {
            attacking = false;
        }
    } 
}

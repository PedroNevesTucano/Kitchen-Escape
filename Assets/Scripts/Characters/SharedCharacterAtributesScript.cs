using UnityEngine;
using TMPro;

public class SharedCharacterAttributesScript : MonoBehaviour
{
    [Header("Shared Attributes / Stats")]
    [SerializeField] public int health;
    [SerializeField] public AttackDistance range;
    [SerializeField] public int attack;
    [SerializeField] public int defence;
    [SerializeField] public bool attacked;
    [SerializeField] public bool usedAbility;
    [SerializeField] public bool walked;
    [SerializeField] public bool allyCharacter;
    [SerializeField] public TextMeshProUGUI DamageIndicatorText;
    [SerializeField] public TextMeshProUGUI CriticalHitText;
    [SerializeField] public AttackType attackType;
    [SerializeField] public Effect currentEffect;
    [SerializeField] public Ability currentAbility;

    public enum AttackType { Circle, Square, Triangle, Diamond };
    public enum AttackDistance { Melee, Ranged };
    public enum Effect { None, Prone, Burning };
    public enum Ability { None, BananaPeel, AcidicScorch, RollingThunder, WeepingShield };

    [Header("Location In Grid")]
    [SerializeField] public int currentGridX;
    [SerializeField] public int currentGridY;

    public void SetGridPosition(int x, int y)
    {
        currentGridX = x;
        currentGridY = y;
    }

    public void ResetWalkingAndAttacking()
    {
        walked = false;
        attacked = false;
    }

    public void GoToSelectedNode(float x, float z)
    {
        transform.position = new Vector3(x, 1.5f, z);
    }
}
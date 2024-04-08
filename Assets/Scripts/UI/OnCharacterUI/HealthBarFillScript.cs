using UnityEngine;
using UnityEngine.UI;

public class HealthBarFillScript : MonoBehaviour
{
    public Image healthBarImage;
    public SharedCharacterAttributesScript sharedAttributesScript;
    public bool found;
    public float maxHP;

    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    void Update()
    {
        if (!found)
        {
            sharedAttributesScript = FindSharedAttributesScript(transform.parent);
            maxHP = sharedAttributesScript.health;
            found = true;
        }

        if (sharedAttributesScript.health <= 0)
        {
            sharedAttributesScript.health = 0;
        }

        float healthPercentage = sharedAttributesScript.health / maxHP;

        if (healthPercentage >= 0.7f)
        {
            healthBarImage.color = Color.green;
        }
        else if (healthPercentage > 0.3f)
        {
            healthBarImage.color = Color.yellow;
        }
        else
        {
            healthBarImage.color = Color.red;
        }

        float fillAmount = CalculateFillAmount(sharedAttributesScript.health);
        healthBarImage.fillAmount = fillAmount;
    }

    float CalculateFillAmount(float currentHealth)
    {
        return currentHealth / maxHP;
    }

    private SharedCharacterAttributesScript FindSharedAttributesScript(Transform currentTransform)
    {
        if (currentTransform == null)
        {
            return null;
        }

        SharedCharacterAttributesScript sharedAttributesScript = currentTransform.GetComponent<SharedCharacterAttributesScript>();
        if (sharedAttributesScript != null)
        {
            return sharedAttributesScript;
        }
        else
        {
            return FindSharedAttributesScript(currentTransform.parent);
        }
    }
}

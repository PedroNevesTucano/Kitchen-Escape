using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuraScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    public MeshRenderer rend;
    public Material playerAura;
    public Material enemyAura;

    private void Start()
    {
        SetMaterial();
    }

    private void Update()
    {

    }

    private void SetMaterial()
    {
        SharedCharacterAttributesScript sharedAttributesScript = FindSharedAttributesScript(transform.parent);
        if (sharedAttributesScript != null)
        {
            // Assuming your SharedCharacterAttributesScript has a boolean property like isEnemy
            if (!sharedAttributesScript.allyCharacter)
            {
                rend.material = enemyAura;
            }
            else
            {
                rend.material = playerAura;
            }
        }
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

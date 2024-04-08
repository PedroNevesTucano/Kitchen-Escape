using UnityEngine;

public class MouseGuide : MonoBehaviour
{
    [Header("Mouse Guide Settings")]

    [SerializeField] private float maxSize = 0.19f;
    [SerializeField] private float minSize = 0.17f;
    [SerializeField] private float growthSpeed = 0.0004f;
    private Renderer rend;

    [Header("Assigned Elements")]

    [SerializeField] public playerInput playerInp;
    [SerializeField] private Material NormalMaterial;
    [SerializeField] private Material HighlightedMaterial;

    [Header("Verifications")]

    [SerializeField] private bool growing = true;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (playerInp != null && playerInp.NodeCheck)
        {
            Vector3 newPosition = new Vector3(playerInp.hitPosition.x, transform.position.y, playerInp.hitPosition.z);
            transform.position = newPosition;
        }

        if (playerInp.CharFound)
        {
            rend.material = HighlightedMaterial;
        }
        else
        {
            rend.material = NormalMaterial;
        }
    }

    void FixedUpdate()
    {
        if (growing)
        {
            if (transform.localScale.x <= maxSize)
            {
                transform.localScale += new Vector3(growthSpeed, 0f, growthSpeed);
            }
            else
            {
                growing = false;
            }
        }
        else
        {
            if (transform.localScale.x >= minSize)
            {
                transform.localScale -= new Vector3(growthSpeed, 0f, growthSpeed);
            }
            else
            {
                growing = true;
            }
        }
    }
}

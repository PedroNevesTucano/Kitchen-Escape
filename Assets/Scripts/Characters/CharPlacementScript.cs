using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharPlacementButtonScript;

public class CharPlacementScript : MonoBehaviour
{
    [Header("Assigned Elements")]

    [SerializeField] public playerInput playerInp;
    [SerializeField] public CharType charType;

    public enum CharType
    {
        Onion,
        Banana,
        Pumpkin,
        Lemon
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInp = FindObjectOfType<playerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInp != null && playerInp.NodeCheck)
        {
            Vector3 newPosition = new Vector3(playerInp.hitPosition.x, transform.position.y, playerInp.hitPosition.z);
            transform.position = newPosition;
        }
    }
}

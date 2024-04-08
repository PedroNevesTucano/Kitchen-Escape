using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameObject WallPrefab;
    [SerializeField] public GameObject CratePrefab;
    [SerializeField] public GameObject BrokenCratePrefab;
    [SerializeField] public GameObject BananaPeelPrefab;
    [SerializeField] public GameObject EnemyPlacementLocationPrefab;
    [SerializeField] public GameObject AllyPlacementLocationPrefab;
    [SerializeField] public GameData gameManager;


    [Header("Verifications")]
    [SerializeField] private bool crateNode;
    [SerializeField] private bool brokenCrateInstantiated;
    [SerializeField] private bool bananaPeelInstantiated;

    private GameObject crateInstance;
    private GameObject bananaPeelInstance;
    private GameObject enemyPlacementNodeInstance;
    private GameObject allyPlacementNodeInstance;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void Start()
    {
        if (gameObject.tag == "Wall")
        {
            Instantiate(WallPrefab, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
        }

        if (gameObject.tag == "Crate")
        {
            crateInstance = Instantiate(CratePrefab, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
            crateNode = true;
        }

        if (gameObject.tag == "EnemyPlacementNode")
        {
            enemyPlacementNodeInstance = Instantiate(EnemyPlacementLocationPrefab, new Vector3(transform.position.x, 0.52f, transform.position.z), Quaternion.identity);
        }

        if (gameObject.tag == "AllyPlacementNode")
        {
            allyPlacementNodeInstance = Instantiate(AllyPlacementLocationPrefab, new Vector3(transform.position.x, 0.52f, transform.position.z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState != GameData.GameState.CharacterPlacement && gameObject.tag == "EnemyPlacementNode")
        {
            Destroy(enemyPlacementNodeInstance);
        }

        if (gameManager.gameState != GameData.GameState.CharacterPlacement && gameObject.tag == "AllyPlacementNode")
        {
            Destroy(allyPlacementNodeInstance);
        }

        if (gameObject.tag == "BananaPeel" && !bananaPeelInstantiated)
        {
            bananaPeelInstance = Instantiate(BananaPeelPrefab, new Vector3(transform.position.x, 1f, transform.position.z), Quaternion.identity);
            bananaPeelInstantiated = true;
        } else if (gameObject.tag != "BananaPeel")
        {
            Destroy(bananaPeelInstance);
        }

        if (crateNode && gameObject.tag != "Crate" && !brokenCrateInstantiated)
        {
            Destroy(crateInstance);
            Instantiate(BrokenCratePrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
            ParticleSystem crateBrakingParticleSystem = gameObject.transform.Find("Crate Braking Particle System").GetComponent<ParticleSystem>();
            if (crateBrakingParticleSystem != null)
            {
                crateBrakingParticleSystem.Play();
            }
            brokenCrateInstantiated = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    [Header("Verifications")]
    [SerializeField] public bool NodeCheck = true;
    [SerializeField] public bool SelectedCharacterBool;
    [SerializeField] public bool CharFound;
    [SerializeField] public bool initialBurningDamage = true;
    [SerializeField] public int turnsBurning;
    [SerializeField] public int turnsProne;
    [SerializeField] private SharedCharacterAttributesScript selectedCharacter = null;

    [Header("Assigned Elements")]
    [SerializeField] public GameObject MouseGuide;
    [SerializeField] public GameObject CharPlacement;
    [SerializeField] private GridManager gridManager;
    [SerializeField] public SharedCharacterAttributesScript characterOnNode;
    [SerializeField] public GameObject walkablePath;
    [SerializeField] public GameObject attackablePath;
    [SerializeField] public GameObject abilityPath;
    [SerializeField] public GameObject Onion;
    [SerializeField] public GameObject Lemon;
    [SerializeField] public GameObject Banana;
    [SerializeField] public GameObject Pumpkin;
    [SerializeField] public GameData gameManager;
    [SerializeField] public AttackButton attackButtonScript;
    [SerializeField] public AbilityButton abilityButtonScript;
    [SerializeField] public TurnIndicatorScript turnScript;

    public Vector3 hitPosition { get; private set; }

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void Update()
    {
        if (gameManager.gameState == GameData.GameState.CharacterPlacement)
        {
            CharPlacementScript charPlacementScriptComponent = FindObjectOfType<CharPlacementScript>();
            if (charPlacementScriptComponent != null)
            {
                CharPlacement = charPlacementScriptComponent.gameObject;
            }
        }

        LayerMask nodeLayerMask = LayerMask.GetMask("Node");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, nodeLayerMask))
        {
            if (!hit.collider.gameObject.CompareTag("Wall"))
            {
                NodeCheck = true;
                hitPosition = hit.collider.gameObject.transform.position;

                string nodeName = hit.collider.gameObject.name;
                int x, y;
                ExtractGridCoordinates(nodeName, out x, out y);

                if (gridManager.IsCharacterOnNode(x, y, out characterOnNode))
                {
                    CharFound = true;
                }
                else
                {
                    CharFound = false;
                }

                // Movementation

                if (Input.GetMouseButtonDown(0) && CharFound && !SelectedCharacterBool && gameManager.gameState == GameData.GameState.PlayerTurn && !attackButtonScript.attacking && !characterOnNode.walked && !abilityButtonScript.usingAbility && characterOnNode.allyCharacter)
                {
                    selectedCharacter = characterOnNode;
                    SelectedCharacterBool = true;

                    List<GameObject> neighbors = gridManager.GetNeighborsWalk(characterOnNode.currentGridX, characterOnNode.currentGridY);
                    foreach (GameObject neighbor in neighbors)
                    {
                        Instantiate(walkablePath, new Vector3(neighbor.transform.position.x, 0.52f, neighbor.transform.position.z), Quaternion.identity);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !CharFound && selectedCharacter && gameManager.gameState == GameData.GameState.PlayerTurn && !attackButtonScript.attacking && !selectedCharacter.walked && !abilityButtonScript.usingAbility && !hit.collider.gameObject.CompareTag("Crate"))
                {
                    List<GameObject> neighbors = gridManager.GetNeighborsWalk(selectedCharacter.currentGridX, selectedCharacter.currentGridY);
                    foreach (GameObject neighbor in neighbors)
                    {
                        if (hit.collider.gameObject == neighbor)
                        {
                            if (hit.collider.gameObject.CompareTag("BananaPeel") && hit.collider.gameObject == neighbor)
                            {
                                selectedCharacter.attacked = true;
                                selectedCharacter.walked = true;
                                selectedCharacter.currentGridX = x;
                                selectedCharacter.currentGridY = y;
                                selectedCharacter.currentEffect = SharedCharacterAttributesScript.Effect.Prone;
                                hit.collider.gameObject.tag = "Untagged";
                                break;
                            }
                            selectedCharacter.currentGridX = x;
                            selectedCharacter.currentGridY = y;
                            selectedCharacter.walked = true;
                            ParticleSystem dustParticleSystem = selectedCharacter.transform.Find("Dust Particle System").GetComponent<ParticleSystem>();
                            if (dustParticleSystem != null)
                            {
                                dustParticleSystem.Play();
                            }
                            break;
                        }
                    }

                    GameObject[] walkablePaths = GameObject.FindGameObjectsWithTag("WalkablePath");
                    foreach (GameObject path in walkablePaths)
                    {
                        Destroy(path);
                    }

                    SelectedCharacterBool = false;
                    selectedCharacter = null;
                }

                // Basic Attack

                if (Input.GetMouseButtonDown(0) && CharFound && !SelectedCharacterBool && gameManager.gameState == GameData.GameState.PlayerTurn && attackButtonScript.attacking && !characterOnNode.attacked && characterOnNode.allyCharacter)
                {
                    selectedCharacter = characterOnNode;
                    SelectedCharacterBool = true;

                    List<GameObject> neighbors = gridManager.GetNeighborsAttack(characterOnNode.currentGridX, characterOnNode.currentGridY);
                    foreach (GameObject neighbor in neighbors)
                    {
                        Instantiate(attackablePath, new Vector3(neighbor.transform.position.x, 0.52f, neighbor.transform.position.z), Quaternion.identity);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && selectedCharacter && gameManager.gameState == GameData.GameState.PlayerTurn && attackButtonScript.attacking && !selectedCharacter.attacked)
                {

                    List<GameObject> neighbors = gridManager.GetNeighborsAttack(selectedCharacter.currentGridX, selectedCharacter.currentGridY);
                    foreach (GameObject neighbor in neighbors)
                    {
                        if (hit.collider.gameObject.CompareTag("Crate") && hit.collider.gameObject == neighbor)
                        {
                            selectedCharacter.attacked = true;
                            attackButtonScript.attacking = false;
                            hit.collider.gameObject.tag = "BrokenCrate";
                            break;
                        }

                        else if (characterOnNode && !characterOnNode.allyCharacter && hit.collider.gameObject == neighbor)
                        {
                            selectedCharacter.attacked = true;
                            attackButtonScript.attacking = false;
                            float effectiveness = 1;

                            switch (selectedCharacter.attackType)
                            {
                                case SharedCharacterAttributesScript.AttackType.Circle:
                                    if (characterOnNode.attackType == SharedCharacterAttributesScript.AttackType.Square)
                                    {
                                        effectiveness = 1.5f;
                                        characterOnNode.CriticalHitText.text = "Critical Hit!";
                                        StartCoroutine(ClearCriticalHitText(characterOnNode));
                                    }
                                    break;
                                case SharedCharacterAttributesScript.AttackType.Square:
                                    if (characterOnNode.attackType == SharedCharacterAttributesScript.AttackType.Triangle)
                                    {
                                        effectiveness = 1.5f;
                                        characterOnNode.CriticalHitText.text = "Critical Hit!";
                                        StartCoroutine(ClearCriticalHitText(characterOnNode));
                                    }
                                    break;
                                case SharedCharacterAttributesScript.AttackType.Triangle:
                                    if (characterOnNode.attackType == SharedCharacterAttributesScript.AttackType.Circle)
                                    {
                                        effectiveness = 1.5f;
                                        characterOnNode.CriticalHitText.text = "Critical Hit!";
                                        StartCoroutine(ClearCriticalHitText(characterOnNode));
                                    }
                                    break;
                                case SharedCharacterAttributesScript.AttackType.Diamond:
                                    break;
                                default:
                                    break;
                            }

                            int damageDealt = (int)((selectedCharacter.attack * effectiveness) - characterOnNode.defence);
                            characterOnNode.health -= damageDealt;

                            StartCoroutine(ShowDamageDealt(damageDealt, characterOnNode));
                            break;
                        }
                    }

                    GameObject[] walkablePaths = GameObject.FindGameObjectsWithTag("WalkablePath");
                    foreach (GameObject path in walkablePaths)
                    {
                        Destroy(path);
                    }

                    attackButtonScript.attacking = false;
                    SelectedCharacterBool = false;
                    selectedCharacter = null;
                }

                //Ability

                if (Input.GetMouseButtonDown(0) && CharFound && !SelectedCharacterBool && gameManager.gameState == GameData.GameState.PlayerTurn && abilityButtonScript.usingAbility && !characterOnNode.usedAbility && characterOnNode.allyCharacter && characterOnNode.currentEffect != SharedCharacterAttributesScript.Effect.Prone)
                {
                    selectedCharacter = characterOnNode;
                    SelectedCharacterBool = true;
                    List<GameObject> neighbors = null;

                    switch (selectedCharacter.currentAbility)
                    {
                        case SharedCharacterAttributesScript.Ability.BananaPeel:
                            neighbors = gridManager.GetNeighborsWalk(characterOnNode.currentGridX, characterOnNode.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.WeepingShield:
                            neighbors = gridManager.GetNeighborsAlly(characterOnNode.currentGridX, characterOnNode.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.AcidicScorch:
                        case SharedCharacterAttributesScript.Ability.RollingThunder:
                            neighbors = gridManager.GetNeighborsAttack(characterOnNode.currentGridX, characterOnNode.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.None:
                            Debug.Log("None");
                            break;
                        default:
                            break;
                    }

                    foreach (GameObject neighbor in neighbors)
                    {
                        Instantiate(abilityPath, new Vector3(neighbor.transform.position.x, 0.52f, neighbor.transform.position.z), Quaternion.identity);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && selectedCharacter && gameManager.gameState == GameData.GameState.PlayerTurn && abilityButtonScript.usingAbility && !selectedCharacter.usedAbility)
                {
                    List<GameObject> neighbors = null;

                    switch (selectedCharacter.currentAbility)
                    {
                        case SharedCharacterAttributesScript.Ability.BananaPeel:
                            neighbors = gridManager.GetNeighborsWalk(selectedCharacter.currentGridX, selectedCharacter.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.WeepingShield:
                            neighbors = gridManager.GetNeighborsAlly(selectedCharacter.currentGridX, selectedCharacter.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.AcidicScorch:
                        case SharedCharacterAttributesScript.Ability.RollingThunder:
                            neighbors = gridManager.GetNeighborsAttack(selectedCharacter.currentGridX, selectedCharacter.currentGridY);
                            break;
                        case SharedCharacterAttributesScript.Ability.None:
                            Debug.Log("None");
                            break;
                        default:
                            break;
                    }

                    foreach (GameObject neighbor in neighbors)
                    {
                        if (hit.collider.gameObject == neighbor)
                        {
                            abilityButtonScript.usingAbility = false;

                            switch (selectedCharacter.currentAbility)
                            {
                                case SharedCharacterAttributesScript.Ability.BananaPeel:
                                    neighbor.tag = "BananaPeel";
                                    selectedCharacter.usedAbility = true;
                                    break;
                                case SharedCharacterAttributesScript.Ability.WeepingShield:
                                    if (characterOnNode && characterOnNode.allyCharacter && hit.collider.gameObject == neighbor)
                                    {
                                        selectedCharacter.defence -= 40;
                                        characterOnNode.defence += 40;
                                        selectedCharacter.usedAbility = true;
                                    }
                                    break;
                                case SharedCharacterAttributesScript.Ability.AcidicScorch:
                                    if (characterOnNode && !characterOnNode.allyCharacter && hit.collider.gameObject == neighbor)
                                    {
                                        characterOnNode.currentEffect = SharedCharacterAttributesScript.Effect.Burning;
                                        selectedCharacter.usedAbility = true;
                                    }
                                    break;
                                case SharedCharacterAttributesScript.Ability.RollingThunder:
                                    if (Random.Range(0f, 1f) < 0.5f)
                                    {
                                        selectedCharacter.health -= 250;
                                        StartCoroutine(ShowDamageDealt(250, selectedCharacter));
                                    }

                                    characterOnNode.health -= 150;
                                    StartCoroutine(ShowDamageDealt(150, characterOnNode));
                                    selectedCharacter.usedAbility = true;
                                    break;
                                case SharedCharacterAttributesScript.Ability.None:
                                    Debug.Log("None");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                    }

                    GameObject[] walkablePaths = GameObject.FindGameObjectsWithTag("WalkablePath");
                    foreach (GameObject path in walkablePaths)
                    {
                        Destroy(path);
                    }

                    abilityButtonScript.usingAbility = false;
                    SelectedCharacterBool = false;
                    selectedCharacter = null;
                }






                if (gameManager.gameState == GameData.GameState.CharacterPlacement && Input.GetMouseButtonDown(1) && !CharPlacement)
                {
                    if (gridManager.IsCharacterOnNode(x, y, out characterOnNode))
                    {
                        GameObject newChar = characterOnNode.gameObject;
                        SharedCharacterAttributesScript CharScript = newChar.GetComponent<SharedCharacterAttributesScript>();
                        CharScript.currentGridX = -1;
                        CharScript.currentGridY = -1;
                        Destroy(characterOnNode.gameObject);
                    }
                }

                if (gameManager.gameState == GameData.GameState.CharacterPlacement && Input.GetMouseButtonDown(0) && CharPlacement != null && hit.collider.gameObject.CompareTag("AllyPlacementNode") && !gridManager.IsCharacterOnNode(x, y, out characterOnNode))
                {
                    CharPlacementScript charPlacementScript = CharPlacement.GetComponent<CharPlacementScript>();

                    switch (charPlacementScript.charType)
                    {
                        case CharPlacementScript.CharType.Lemon:
                            setCharType(CharPlacementScript.CharType.Lemon, hit.collider.gameObject.transform.position, x, y);
                            break;
                        case CharPlacementScript.CharType.Banana:
                            setCharType(CharPlacementScript.CharType.Banana, hit.collider.gameObject.transform.position, x, y);
                            break;
                        case CharPlacementScript.CharType.Pumpkin:
                            setCharType(CharPlacementScript.CharType.Pumpkin, hit.collider.gameObject.transform.position, x, y);
                            break;
                        case CharPlacementScript.CharType.Onion:
                            setCharType(CharPlacementScript.CharType.Onion, hit.collider.gameObject.transform.position, x, y);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                NodeCheck = false;
            }
        }
        else
        {
            NodeCheck = false;
        }

        if (gameManager.gameState == GameData.GameState.CharacterPlacement && Input.GetMouseButtonDown(1) && CharPlacement != null)
        {
            Destroy(CharPlacement);
        }

        if (IsCharacterBurning())
        {
            SharedCharacterAttributesScript[] characters = FindObjectsOfType<SharedCharacterAttributesScript>();
            foreach (SharedCharacterAttributesScript character in characters)
            {
                if (character.currentEffect == SharedCharacterAttributesScript.Effect.Burning)
                {
                    if (character.health > 0 && (gameManager.gameState != turnScript.previousState) || initialBurningDamage)
                    {
                        character.health -= 40;
                        StartCoroutine(ShowDamageDealt(40, character));
                        initialBurningDamage = false;
                        turnsBurning++;
                    }
                    if (turnsBurning >= 5)
                    {
                        turnsBurning = 0;
                        initialBurningDamage = true;
                        character.currentEffect = SharedCharacterAttributesScript.Effect.None;
                    }
                }
            }
        }

        if (IsCharacterProne())
        {
            SharedCharacterAttributesScript[] characters = FindObjectsOfType<SharedCharacterAttributesScript>();
            foreach (SharedCharacterAttributesScript character in characters)
            {
                if (character.currentEffect == SharedCharacterAttributesScript.Effect.Prone)
                {
                    if (gameManager.gameState != turnScript.previousState)
                    {
                        turnsProne++;
                    }
                    if (turnsProne >= 3)
                    {
                        turnsProne = 0;
                        character.currentEffect = SharedCharacterAttributesScript.Effect.None;
                    }
                }
            }
        }

        MouseGuide.SetActive(NodeCheck && !SelectedCharacterBool && gameManager.gameState != GameData.GameState.Cutscene);
        if (CharPlacement != null)
        {
            CharPlacement.SetActive(NodeCheck && !SelectedCharacterBool);
        }
    }

    void ExtractGridCoordinates(string nodeName, out int x, out int y)
    {
        string[] parts = nodeName.Split('_');
        x = int.Parse(parts[1]);
        y = int.Parse(parts[2]);
    }

    IEnumerator ShowDamageDealt(int damage, SharedCharacterAttributesScript character)
    {
        character.DamageIndicatorText.text = "-" + damage.ToString();
        yield return new WaitForSeconds(2f);
        character.DamageIndicatorText.text = "";
    }

    IEnumerator ClearCriticalHitText(SharedCharacterAttributesScript character)
    {
        yield return new WaitForSeconds(2f);
        character.CriticalHitText.text = "";
    }

    bool IsCharacterBurning()
    {
        SharedCharacterAttributesScript[] characters = FindObjectsOfType<SharedCharacterAttributesScript>();

        foreach (SharedCharacterAttributesScript character in characters)
        {
            if (character.currentEffect == SharedCharacterAttributesScript.Effect.Burning)
            {
                return true;
            }
        }
        return false;
    }

    bool IsCharacterProne()
    {
        SharedCharacterAttributesScript[] characters = FindObjectsOfType<SharedCharacterAttributesScript>();

        foreach (SharedCharacterAttributesScript character in characters)
        {
            if (character.currentEffect == SharedCharacterAttributesScript.Effect.Prone)
            {
                return true;
            }
        }
        return false;
    }

    public void setCharType(CharPlacementScript.CharType charType, Vector3 position, int x, int y)
    {
        GameObject newChar;
        switch (charType)
        {
            case CharPlacementScript.CharType.Lemon:
                newChar = Instantiate(Lemon, position, Quaternion.identity);
                break;
            case CharPlacementScript.CharType.Banana:
                newChar = Instantiate(Banana, position, Quaternion.identity);
                break;
            case CharPlacementScript.CharType.Pumpkin:
                newChar = Instantiate(Pumpkin, position, Quaternion.identity);
                break;
            case CharPlacementScript.CharType.Onion:
                newChar = Instantiate(Onion, position, Quaternion.identity);
                break;
            default:
                newChar = null;
                break;
        }

        if (newChar != null)
        {
            Destroy(CharPlacement);

            SharedCharacterAttributesScript charScript = newChar.GetComponent<SharedCharacterAttributesScript>();
            if (charScript != null)
            {
                charScript.currentGridX = x;
                charScript.currentGridY = y;
            }
        }
    }
}
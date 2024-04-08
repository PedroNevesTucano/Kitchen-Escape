using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Verifications")]

    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 6;
    [SerializeField] private List<List<GameObject>> nodesGrid;
    
    [Header("Assigned Elements")]
    [SerializeField] private List<SharedCharacterAttributesScript> characters;

    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        FindCharacters();
        foreach (SharedCharacterAttributesScript character in characters)
        {
            //Debug.Log(character.gameObject.name + " grid position: (" + character.currentGridX + ", " + character.currentGridY + ")");
        }
    }

    void CreateGrid()
    {
        nodesGrid = new List<List<GameObject>>();

        for (int x = 0; x < gridWidth; x++)
        {
            nodesGrid.Add(new List<GameObject>());
            for (int y = 0; y < gridHeight; y++)
            {
                string nodeName = "Node_" + x + "_" + y;
                GameObject nodeObj = GameObject.Find(nodeName);

                if (nodeObj != null)
                {
                    nodesGrid[x].Add(nodeObj);
                }
                else
                {
                    Debug.LogError("Node GameObject not found at position: " + x + ", " + y);
                }
            }
        }
    }

    public bool IsCharacterOnNode(int x, int y, out SharedCharacterAttributesScript character)
    {
        foreach (var chara in characters)
        {
            if (chara.currentGridX == x && chara.currentGridY == y)
            {
                character = chara;
                return true;
            }
        }
        character = null;
        return false;
    }

    void FindCharacters()
    {
        SharedCharacterAttributesScript[] foundCharacters = FindObjectsOfType<SharedCharacterAttributesScript>();

        foreach (var foundCharacter in foundCharacters)
        {
            if (!characters.Contains(foundCharacter))
            {
                characters.Add(foundCharacter);
            }
        }
    }

    public List<GameObject> GetNeighborsWalk(int x, int y)
    {
        List<GameObject> neighbors = new List<GameObject>();

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y)
                    continue;

                if (i >= 0 && i < gridWidth && j >= 0 && j < gridHeight)
                {
                    GameObject neighborNode = nodesGrid[i][j];

                    // Check if the neighbor node is a wall, crate, or has a character on it
                    if (neighborNode.CompareTag("Wall") || neighborNode.CompareTag("Crate") || IsCharacterOnNode(i, j, out SharedCharacterAttributesScript _))
                        continue;

                    neighbors.Add(neighborNode);
                }
            }
        }
        return neighbors;
    }

    public List<GameObject> GetNeighborsAttack(int x, int y)
    {
        List<GameObject> neighbors = new List<GameObject>();

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y)
                    continue;

                if (i >= 0 && i < gridWidth && j >= 0 && j < gridHeight)
                {
                    GameObject neighborNode = nodesGrid[i][j];

                    if (neighborNode.CompareTag("Wall"))
                        continue;

                    SharedCharacterAttributesScript characterOnNode;
                    if (IsCharacterOnNode(i, j, out characterOnNode))
                    {
                        if (characterOnNode.allyCharacter)
                            continue;
                    }

                    neighbors.Add(neighborNode);
                }
            }
        }
        return neighbors;
    }

    public List<GameObject> GetNeighborsAlly(int x, int y)
    {
        List<GameObject> neighbors = new List<GameObject>();

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y)
                    continue;

                if (i >= 0 && i < gridWidth && j >= 0 && j < gridHeight)
                {
                    GameObject neighborNode = nodesGrid[i][j];

                    if (neighborNode.CompareTag("Wall"))
                        continue;

                    SharedCharacterAttributesScript characterOnNode;
                    if (IsCharacterOnNode(i, j, out characterOnNode))
                    {
                        if (!characterOnNode.allyCharacter)
                            continue;
                    }

                    neighbors.Add(neighborNode);
                }
            }
        }
        return neighbors;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Verifications")]
    [SerializeField] public GameState gameState;
    [SerializeField] public bool lvl1Complete;
    [SerializeField] public bool lvl2Complete;
    [SerializeField] public bool lvl3Complete;

    public enum GameState
    {
        CharacterPlacement,
        Cutscene,
        PlayerTurn,
        EnemyTurn,
        GameOver,
        GameWon
    }

    private KeyCode validKeyPress = KeyCode.Mouse0;

    public KeyCode GetValidKeyPress()
    {
        return validKeyPress;
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameData");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

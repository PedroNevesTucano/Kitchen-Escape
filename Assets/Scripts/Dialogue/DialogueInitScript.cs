using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInitScript : MonoBehaviour
{
    [Header("Assigned Elements")]
    [SerializeField] public GameData gameManager;

    [Header("Verifications")]
    [SerializeField] public bool asigned;



    public List<CameraDialogue.DialogueEntry> dialogueStart;
    //public CameraDialogue.DialogueEntry dialogueBase;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void InitializeDialogue()
    {
        Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>{
            { "Lemon", GameObject.Find("Lemon(Clone)") },
            { "Onion", GameObject.Find("Onion(Clone)") },
            { "Banana", GameObject.Find("Banana(Clone)") },
            { "Pumpkin", GameObject.Find("Pumpkin(Clone)") },
            //{ "char3", GameObject.Find("Char3") },
        };

        Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>{
            { "OBJ1", GameObject.Find("Key Location 1") },
            { "OBJ2", GameObject.Find("Key Location 2") },
            { "OBJ3", GameObject.Find("Key Location 3") },
        };

        dialogueStart = new List<CameraDialogue.DialogueEntry>
        {
            new CameraDialogue.DialogueEntry(characters["Banana"], "The <color=purple>(Onion)</color> character is going to speak, the camera will pan out to display him.", CameraDialogue.DialogueEntry.Emotion.Neutral),
            new CameraDialogue.DialogueEntry(characters["Onion"], "Hello, this cutscene system is working, and it did not take me hours to fix a bool that was <color=green>(bugging)</color> everything :).", CameraDialogue.DialogueEntry.Emotion.Happy),
            new CameraDialogue.DialogueEntry(characters["Pumpkin"], "I AM A PUMPKIN.", CameraDialogue.DialogueEntry.Emotion.Happy),
            /*new CameraDialogue.DialogueEntry(null, objects["OBJ2"], "", CameraDialogue.DialogueEntry.Emotion.Neutral),
            new CameraDialogue.DialogueEntry(characters["char2"], "That was a specific <color=orange>(key location!)</color> characters can sometimes not coment on them.", CameraDialogue.DialogueEntry.Emotion.Sad),
            new CameraDialogue.DialogueEntry(characters["char2"], objects["OBJ3"] , "but other times they can, this is <color=red>(me)</color> presenting another <color=orange>(key location)</color>.", CameraDialogue.DialogueEntry.Emotion.Angry),
            new CameraDialogue.DialogueEntry(characters["char2"], "Bye Bye.", CameraDialogue.DialogueEntry.Emotion.Happy)*/
        };
    }

    private void Update()
    {
        if (gameManager.gameState == GameData.GameState.Cutscene && !asigned)
        {
            InitializeDialogue();
            asigned = true;
        }
    }
}

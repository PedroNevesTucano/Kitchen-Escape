using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CameraDialogue;

public class SpeakerImageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public RawImage image;
    public CameraDialogue cam;

    [Header("Assigned Elements")]
    [SerializeField] public GameData gameManager;

    [System.Serializable]
    public class Character
    {
        public string characterName; // Name of the character
        public List<Emotion> emotions = new List<Emotion>();

        [System.Serializable]
        public class Emotion
        {
            public DialogueEntry.Emotion Name;
            public Texture2D Image;
        }
    }

    [SerializeField] private List<Character> characterList = new List<Character>();

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void Start()
    {
        // You can search for instantiated characters here if needed
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam.stop && gameManager.gameState == GameData.GameState.Cutscene)
        {
            foreach (Character character in characterList)
            {
                GameObject characterObject = GameObject.Find(character.characterName);
                if (characterObject != null)
                {
                    if (characterObject == cam.currentCharacter)
                    {
                        if (character.emotions != null)
                        {
                            foreach (Character.Emotion emotion in character.emotions)
                            {
                                if (emotion != null && emotion.Name == cam.currentEntry.emotion)
                                {
                                    image.texture = emotion.Image;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

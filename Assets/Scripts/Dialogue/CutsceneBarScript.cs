using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CutsceneBarScript : MonoBehaviour
{
    [Header("Cutscene Bars Settings")]
    [SerializeField] private float followSpeed = 10f;

    [Header("TopBar")]
    [SerializeField] private RectTransform topBarRectTransform;
    [SerializeField] private float topBarLoweredPosY = 210;

    [Header("BottomBar")]
    [SerializeField] private RectTransform bottomBarRectTransform;
    [SerializeField] private float bottomBarRisenPosY = -210;

    [Header("BarSelection")]
    [SerializeField] private bool topBar;

    [Header("Assigned Elements")]
    public GameData gameManager;
    public CameraDialogue cam;

    private Vector3 topBarOriginalPos;
    private Vector3 bottomBarOriginalPos;

    public GameObject cutsceneBars;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }
    private void Start()
    {
        // Set the original positions of bars
        topBarOriginalPos = topBarRectTransform.anchoredPosition;
        bottomBarOriginalPos = bottomBarRectTransform.anchoredPosition;
        cutsceneBars = GameObject.FindWithTag("Cutscene Bars");
    }

    private void FixedUpdate()
    {
        if (gameManager.gameState == GameData.GameState.Cutscene)
        {
            if (topBar && !cam.stop)
            {
                Vector3 targetPos = new Vector3(topBarRectTransform.anchoredPosition.x, topBarLoweredPosY, 0f);
                topBarRectTransform.anchoredPosition = Vector3.Lerp(topBarRectTransform.anchoredPosition, targetPos, Time.fixedDeltaTime * followSpeed);
            }
            else if (!topBar && !cam.stop)
            {
                Vector3 targetPosBottom = new Vector3(bottomBarRectTransform.anchoredPosition.x, bottomBarRisenPosY, 0f);
                bottomBarRectTransform.anchoredPosition = Vector3.Lerp(bottomBarRectTransform.anchoredPosition, targetPosBottom, Time.fixedDeltaTime * followSpeed);
            }

            if (cam.stop)
            {
                topBarRectTransform.anchoredPosition = Vector3.Lerp(topBarRectTransform.anchoredPosition, topBarOriginalPos, Time.fixedDeltaTime * followSpeed);
                bottomBarRectTransform.anchoredPosition = Vector3.Lerp(bottomBarRectTransform.anchoredPosition, bottomBarOriginalPos, Time.fixedDeltaTime * followSpeed);

                if (Vector3.Distance(topBarRectTransform.anchoredPosition, topBarOriginalPos) < 0.8f && Vector3.Distance(bottomBarRectTransform.anchoredPosition, bottomBarOriginalPos) < 0.8f)
                {
                    cutsceneBars.SetActive(false);
                }

            }
        }
    }
}

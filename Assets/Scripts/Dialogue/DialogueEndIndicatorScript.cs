using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEndIndicatorScript : MonoBehaviour
{
    [Header("Dialogue End Indicator Settings")]
    [SerializeField] private float maxTopPosition = - 203;
    [SerializeField] private float maxBottomPosition = - 207;
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private bool moveUp = true;

    [Header("Assigned Elements")]
    public GameData gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
    }

    void FixedUpdate()
    {
        if (gameManager.gameState == GameData.GameState.Cutscene)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 currentPosition = rectTransform.anchoredPosition;

            float targetY = moveUp ? maxTopPosition : maxBottomPosition;
            currentPosition.y = Mathf.Lerp(currentPosition.y, targetY, Time.fixedDeltaTime * followSpeed);

            rectTransform.anchoredPosition = currentPosition;

            if (Mathf.Abs(currentPosition.y - targetY) < 0.6f)
            {
                moveUp = !moveUp;
            }
        }
    }
}

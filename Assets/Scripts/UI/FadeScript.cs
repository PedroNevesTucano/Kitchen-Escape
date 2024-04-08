using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [Header("Verifications")]
    [SerializeField] private float fadeInOutDuration = 1.5f;
    [SerializeField] private float currentAlpha = 1f;

    private UnityEngine.UI.Image image;
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();

        SetImageAlpha(1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentAlpha -= Time.fixedDeltaTime / fadeInOutDuration;

        currentAlpha = Mathf.Clamp01(currentAlpha);

        SetImageAlpha(currentAlpha);
    }

    void SetImageAlpha(float alpha)
    {
        Color currentColor = image.color;

        currentColor.a = alpha;

        image.color = currentColor;
    }
}

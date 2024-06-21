using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public float smoothness = 0.96f;
    public float curvature = 1f;
    public float time = 0f;
    public float fadeOutDelay = 0f;
    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;

    private Image blinkImage;
    private float timer;
    private bool isBlinking;

    void Start()
    {
        blinkImage = GetComponent<Image>();
        timer = time;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !isBlinking)
        {
            StartCoroutine(Blink());
            timer = time;
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;

        // Fade in
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * smoothness;
            blinkImage.color = new Color(0, 0, 0, fadeInCurve.Evaluate(t));
            yield return null;
        }

        // Wait for the fade-out delay
        yield return new WaitForSeconds(fadeOutDelay);

        // Fade out
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * smoothness;
            blinkImage.color = new Color(0, 0, 0, fadeOutCurve.Evaluate(t));
            yield return null;
        }

        isBlinking = false;
    }
}

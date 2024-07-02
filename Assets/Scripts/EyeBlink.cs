using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EyeBlink : MonoBehaviour
{
    public RectTransform upperBox;
    public RectTransform lowerBox;
    public float speed = 0.70f;
    public int blinkTimes = 3;
    public bool endClosing = false;
    public PostProcessVolume postProcessVolume; // Reference to PostProcessVolume component
    private DepthOfField dof; // Reference to DepthOfField settings

    private Vector3 originalUpperPosition;
    private Vector3 originalLowerPosition;
    private Vector3 endUpper;
    private Vector3 endLower;

    private int currentBlink = 1;

    public enum Action
    {
        Open,
        Close
    };

    void Awake()
    {
        originalUpperPosition = upperBox.position;
        originalLowerPosition = lowerBox.position;

        // Get DepthOfField settings from PostProcessVolume
        postProcessVolume.profile.TryGetSettings(out dof);
    }

    void OnEnable()
    {
        StartCoroutine(blink());
    }

    private IEnumerator blink()
    {
        while (currentBlink <= blinkTimes)
        {
            endUpper = originalUpperPosition;
            endLower = originalLowerPosition;

            endUpper.y += (50 * currentBlink);
            endLower.y -= (50 * currentBlink);

            // Open eyelids
            yield return moveEyelids(endUpper, endLower, Action.Open);

            // Check if we want to end the blink closing the eyes
            if (currentBlink == blinkTimes && !endClosing)
            {
                originalUpperPosition.y = Screen.height * 2;
                originalLowerPosition.y = -Screen.height;
            }

            // Close eyelids
            yield return moveEyelids(originalUpperPosition, originalLowerPosition, Action.Close);

            currentBlink++;
        }
    }

    private IEnumerator moveEyelids(Vector3 upperLid, Vector3 lowerLid, Action action)
    {
        float elapsedTime = 0;

        // Adjust depth of field effect
        if (action == Action.Open)
        {
            StartCoroutine(AdjustBlur(true));
        }
        else
        {
            StartCoroutine(AdjustBlur(false));
        }

        while (elapsedTime < speed)
        {
            float duration = (elapsedTime / speed);

            if (action == Action.Open)
            {
                upperBox.position = Vector3.Lerp(originalUpperPosition, upperLid, duration);
                lowerBox.position = Vector3.Lerp(originalLowerPosition, lowerLid, duration);
            }
            else
            {
                upperBox.position = Vector3.Lerp(endUpper, upperLid, duration);
                lowerBox.position = Vector3.Lerp(endLower, lowerLid, duration);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator AdjustBlur(bool blur)
    {
        float targetFocusDistance = blur ? 0.1f : 10f; // Example values, adjust as needed
        float targetAperture = blur ? 5.6f : 5.6f; // Example values, adjust as needed
        float targetFocalLength = blur ? 50f : 50f; // Example values, adjust as needed

        float initialFocusDistance = dof.focusDistance.value;
        float initialAperture = dof.aperture.value;
        float initialFocalLength = dof.focalLength.value;

        float duration = 0.5f; // Adjustment duration
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate values over time
            dof.focusDistance.value = Mathf.Lerp(initialFocusDistance, targetFocusDistance, elapsedTime / duration);
            dof.aperture.value = Mathf.Lerp(initialAperture, targetAperture, elapsedTime / duration);
            dof.focalLength.value = Mathf.Lerp(initialFocalLength, targetFocalLength, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        // Ensure final values are set
        dof.focusDistance.value = targetFocusDistance;
        dof.aperture.value = targetAperture;
        dof.focalLength.value = targetFocalLength;
    }
}

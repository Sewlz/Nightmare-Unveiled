using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class JumpscareTrigger : MonoBehaviour
{
    public GameObject animationObject; // The object that will play the animation during the jumpscare
    public AudioClip jumpscareSound; // The sound that will play during the jumpscare
    public AudioClip scaredBreath; // The sound of scared breathing
    public float scareVolume = 1.0f;
    public float scaredBreathTime = 2.0f;

    public bool enableEffects = true;
    public PostProcessVolume scareEffects;
    public float chromaticAberrationAmount = 1.0f;
    public float vignetteAmount = 0.5f;
    public float effectsTime = 2.0f;

    public bool scareShake = true;
    public bool shakeByPreset = false;
    public float magnitude = 1.0f;
    public float roughness = 2.0f;
    public float startTime = 0.1f;
    public float durationTime = 1.0f;
    public Vector3 positionInfluence = Vector3.one;
    public Vector3 rotationInfluence = Vector3.one;

    private AudioSource audioSource;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private bool isJumpscareActive = false;

    private Animator animator;

    void Start()
    {
        if (animationObject != null)
        {
            animationObject.SetActive(false);
            animator = animationObject.GetComponent<Animator>();
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (scareEffects != null && enableEffects)
        {
            scareEffects.profile.TryGetSettings(out chromaticAberration);
            scareEffects.profile.TryGetSettings(out vignette);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isJumpscareActive)
        {
            isJumpscareActive = true;
            ActivateJumpscare();
        }
    }

    void ActivateJumpscare()
    {
        if (animationObject != null)
        {
            animationObject.SetActive(true);
            if (animator != null)
            {
                animator.SetTrigger("Jumpscare"); // Assumes there's a trigger parameter named "Jumpscare" in the Animator
            }
        }

        if (jumpscareSound != null)
        {
            audioSource.PlayOneShot(jumpscareSound, scareVolume);
        }

        if (scaredBreath != null)
        {
            Invoke("PlayScaredBreath", scaredBreathTime);
        }

        if (enableEffects && scareEffects != null)
        {
            StartCoroutine(ApplyEffects());
        }

        if (scareShake)
        {
            if (shakeByPreset)
            {
                // Implement shake by preset here if you have predefined presets
                StartCoroutine(CameraShakePreset());
            }
            else
            {
                StartCoroutine(CameraShake());
            }
        }
    }

    void PlayScaredBreath()
    {
        if (scaredBreath != null)
        {
            audioSource.PlayOneShot(scaredBreath, scareVolume);
        }
    }

    IEnumerator ApplyEffects()
    {
        float elapsedTime = 0f;

        while (elapsedTime < effectsTime)
        {
            if (chromaticAberration != null)
            {
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromaticAberrationAmount, elapsedTime / effectsTime);
            }

            if (vignette != null)
            {
                vignette.intensity.value = Mathf.Lerp(0, vignetteAmount, elapsedTime / effectsTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(effectsTime);

        // Reset effects after duration
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = 0;
        }

        if (vignette != null)
        {
            vignette.intensity.value = 0;
        }

        if (animationObject != null)
        {
            animationObject.SetActive(false);
        }
    }

    IEnumerator CameraShake()
    {
        float elapsedTime = 0f;
        Vector3 originalPos = Camera.main.transform.localPosition;
        Quaternion originalRot = Camera.main.transform.localRotation;

        while (elapsedTime < durationTime)
        {
            float x = Random.Range(-1f, 1f) * magnitude * positionInfluence.x;
            float y = Random.Range(-1f, 1f) * magnitude * positionInfluence.y;
            float z = Random.Range(-1f, 1f) * magnitude * positionInfluence.z;

            float rotX = Random.Range(-1f, 1f) * magnitude * rotationInfluence.x;
            float rotY = Random.Range(-1f, 1f) * magnitude * rotationInfluence.y;
            float rotZ = Random.Range(-1f, 1f) * magnitude * rotationInfluence.z;

            Camera.main.transform.localPosition = originalPos + new Vector3(x, y, z);
            Camera.main.transform.localRotation = originalRot * Quaternion.Euler(rotX, rotY, rotZ);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
        Camera.main.transform.localRotation = originalRot;
    }

    IEnumerator CameraShakePreset()
    {
        // Implement your camera shake preset logic here
        // This is a placeholder for demonstration
        yield return CameraShake();
    }
}

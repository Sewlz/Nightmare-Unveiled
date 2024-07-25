using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class AfterCreditsHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public Animator creditsAnimator; 
    public TextMeshProUGUI blinkingText; 
    public AudioSource backgroundMusic; 
    public RawImage videoRawImage; 

    private bool isCreditsFinished = false;

    void Start()
    {
        blinkingText.gameObject.SetActive(false);
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
        creditsAnimator.enabled = false;
    }

    void Update()
    {
        if (isCreditsFinished && Input.GetMouseButtonDown(0))
        {
            LoadMainMenu();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        if (videoRawImage != null)
        {
            videoRawImage.gameObject.SetActive(false);
        }

        StartCoroutine(PlayAnimatorAfterVideo());
    }

    private IEnumerator PlayAnimatorAfterVideo()
    {
        yield return null;
        creditsAnimator.enabled = true;
        creditsAnimator.Play("AfterCreditg", -1, 0f);
        yield return StartCoroutine(CheckCreditsEnd());
        blinkingText.gameObject.SetActive(true);
        StartCoroutine(BlinkText());
    }

    private IEnumerator CheckCreditsEnd()
    {
        AnimatorStateInfo stateInfo = creditsAnimator.GetCurrentAnimatorStateInfo(0);
        while (stateInfo.normalizedTime < 1f || creditsAnimator.IsInTransition(0))
        {
            yield return null;
            stateInfo = creditsAnimator.GetCurrentAnimatorStateInfo(0);
        }
        isCreditsFinished = true;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            blinkingText.text = "Click to back main menu";
            yield return new WaitForSeconds(0.5f);
            blinkingText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}

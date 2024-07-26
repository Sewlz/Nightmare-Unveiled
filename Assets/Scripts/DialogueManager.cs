using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
    public AudioSource carSound;
    public AudioSource crashSound;
    public RawImage warningImage; // RawImage cho hieu ung fade-out
    public VideoPlayer videoPlayer; // VideoPlayer dau tien
    public RawImage videoPlayerRawImage; // RawImage chua video dau tien
    public VideoPlayer videoPlayer2; // VideoPlayer thu hai
    public RawImage videoPlayer2RawImage; // RawImage chua video thu hai
    public VideoPlayer videoPlayer3; // VideoPlayer thu ba
    public RawImage videoPlayer3RawImage; // RawImage chua video thu ba
    public VideoPlayer videoPlayer4; // VideoPlayer thu bon
    public RawImage videoPlayer4RawImage; // RawImage chua video thu bon
    public int typingSpeedFactor = 5; // He so toc do hien thi

    private string[] dialogues = {
        "My name is Nguyen Thanh Nhat Minh. I am currently a student at Van Lang University, majoring in Software Engineering, and at the same time, I am a YouTuber specializing in content related to horror games and filming videos in the form of found footage.",
        "One day, while I was resting at home, I received an anonymous message about an abandoned industrial area.",
        "According to the information I found, that place had experienced a shocking event that caused many people to die and go missing, and until now, there have been no answers for the phenomenon.",
        "After reading the message, I felt extremely intrigued and decided to start this trip. After preparing everything, I set off in the old car that my family had left me.",
        "The journey was quite long, and the industrial area was deep in the forest. While traveling through the forest, things seemed to go awry.",
        "It rained heavily, and the car seemed to be running out of gas. Being in the forest, there was no place to stop and check, as if an invisible force was trying to prevent me from reaching the place.",
        "When I reached a fork in the road, a shadowy figure suddenly appeared right in front of the car.",
        "Panicking, I hit the brakes, but it seemed the brakes were broken, so I turned the wheel sharply in another direction.",
        "At that moment, everything seemed to go dark, and I gradually slipped into unconsciousness..."
    };

    private int currentDialogueIndex = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        nextButton.onClick.AddListener(NextDialogue);
        carSound.Play();
        warningImage.color = new Color(0, 0, 0, 0); // Dam bao warningImage an luc dau
        warningImage.raycastTarget = false; // Tat Raycast Target de khong chon cac tuong tac voi Button
        videoPlayerRawImage.gameObject.SetActive(false); // An video dau tien luc bat dau
        videoPlayer2RawImage.gameObject.SetActive(false); // An video thu hai luc bat dau
        videoPlayer3RawImage.gameObject.SetActive(false); // An video thu ba luc bat dau
        videoPlayer4RawImage.gameObject.SetActive(false); // An video thu tu luc bat dau
        PlayVideo(); // Phat video 1 tu dau doan hoi thoai
        StartTyping(dialogues[currentDialogueIndex]); // Bat dau hien thi doan hoi thoai dau tien
    }

    void NextDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Dung Coroutine hien tai neu co
        }

        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            StartTyping(dialogues[currentDialogueIndex]); // Bat dau hien thi doan hoi thoai moi

            // Kiem tra neu den doan hoi thoai muon dung video dau tien va chuyen sang video thu hai
            if (dialogues[currentDialogueIndex] == "After reading the message, I felt extremely intrigued and decided to start this trip. After preparing everything, I set off in the old car that my family had left me.")
            {
                StopVideo(); // Dung video 1 neu dang phat
                PlayVideo2(); // Phat video 2
            }
            else if (dialogues[currentDialogueIndex] == "When I reached a fork in the road, a shadowy figure suddenly appeared right in front of the car.")
            {
                StopVideo2(); // Dung video 2 neu dang phat
                PlayVideo3(); // Phat video 3
            }
            // Kiem tra neu den doan hoi thoai muon dung video thu hai va chuyen sang video thu ba
            else if (dialogues[currentDialogueIndex] == "Panicking, I hit the brakes, but it seemed the brakes were broken, so I turned the wheel sharply in another direction.")
            {
                StopVideo3(); // Dung video 2 neu dang phat
                PlayVideo4(); // Phat video 3
                PlayCrashSound(); // Phat am thanh crash car cung luc voi video 3
            }
        }

        // Kiem tra neu la doan hoi thoai cuoi cung
        if (currentDialogueIndex == dialogues.Length - 1)
        {
            nextButton.gameObject.SetActive(false); // An nut nextButton khi den doan hoi thoai cuoi cung
            StartCoroutine(FadeOutAndLoadNextScene()); // Bat dau fade-out va sau do load scene moi
        }
    }

    void PlayVideo()
    {
        videoPlayerRawImage.gameObject.SetActive(true); // Hien thi video dau tien
        videoPlayer.Play();
    }

    void StopVideo()
    {
        videoPlayer.Stop();
        videoPlayerRawImage.gameObject.SetActive(false); // An video dau tien sau khi dung
    }

    void PlayVideo2()
    {
        videoPlayer2RawImage.gameObject.SetActive(true); // Hien thi video thu hai
        videoPlayer2.Play();
    }

    void StopVideo2()
    {
        videoPlayer2.Stop();
        videoPlayer2RawImage.gameObject.SetActive(false); // An video thu hai sau khi dung
    }

    void PlayCrashSound()
    {
        crashSound.Play(); // Phat am thanh crash car
    }

    void PlayVideo3()
    {
        videoPlayer3RawImage.gameObject.SetActive(true); // Hien thi video thu ba
        videoPlayer3.Play();
    }
    void StopVideo3()
    {
        videoPlayer3.Stop();
        videoPlayer3RawImage.gameObject.SetActive(false);
    }
    void PlayVideo4()
    {
        videoPlayer4RawImage.gameObject.SetActive(true); // Hien thi video thu ba
        videoPlayer4.Play();
    }

    void LoadNextScene()
    {
        PlayerPrefs.SetString("NextScene", "Map1");
        SceneManager.LoadScene("LoadingScreen");
    }

    IEnumerator FadeOutAndLoadNextScene()
    {
        float duration = 4f; // Thoi gian de man hinh toi dan
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            warningImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, elapsed / duration));
            yield return null;
        }

        warningImage.color = new Color(0, 0, 0, 1f);

        // Cho cho du thoi gian fade-out (tuong ung voi duration)
        yield return new WaitForSeconds(duration);

        LoadNextScene(); // Load scene moi sau khi fade-out hoan tat
    }

    void StartTyping(string dialogue)
    {
        typingCoroutine = StartCoroutine(TypeSentence(dialogue));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        int totalCharacters = sentence.Length;
        int displayedCharacters = 0;

        while (displayedCharacters < totalCharacters)
        {
            dialogueText.text = sentence.Substring(0, displayedCharacters + 1);
            displayedCharacters++;
            yield return new WaitForSeconds(typingSpeedFactor * 0.01f); // Dieu chinh toc do hien thi
        }
    }
}

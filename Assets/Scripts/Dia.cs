using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class Dia : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;
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
        "After overcoming a series of intense challenges in the hospital area, I finally collected enough fuses to restart the power. Hope surged as I realized the emergency exit door, which had been locked due to a lack of energy, could now be opened. " ,
        "With salvation so close, I rushed towards the exit, my feet barely touching the ground, my heart pounding fiercely in my chest.",

        "But luck did not come as easily as I had hoped. " ,

        "The growling of the monster echoed behind me, and it was rapidly closing the distance between us. Every step I took, every ragged breath was overshadowed by the threat from the pursuing enemy. " ,
        "I felt its presence growing ever closer, the fear driving me forward.With my last ounce of strength, I hurled myself toward the exit, leaping into the void of salvation like a final glimmer of hope." ,
        "Everything around me suddenly became blurred, and the space was gradually enveloped in darkness.",

        "When the light returned, I opened my eyes to find myself lying on a hospital bed, the fluorescent lights shining directly into my eyes. Familiar hospital images surrounded me, and I felt as if I had just emerged from a nightmare." ,
        " At this moment, I was left with a vague sense of the battle that had passed and a profound question: Is this a new beginning or just another part of the nightmare from which I have yet to escape?"
    };

    private int currentDialogueIndex = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        nextButton.onClick.AddListener(NextDialogue);
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
            if (dialogues[currentDialogueIndex] == "But luck did not come as easily as I had hoped. ")
            {
                StopVideo(); // Dung video 1 neu dang phat
                PlayVideo2(); // Phat video 2
            }
            else if (dialogues[currentDialogueIndex] == "The growling of the monster echoed behind me, and it was rapidly closing the distance between us. Every step I took, every ragged breath was overshadowed by the threat from the pursuing enemy. ")
            {
                StopVideo2(); // Dung video 2 neu dang phat
                PlayVideo3(); // Phat video 3
            }
            // Kiem tra neu den doan hoi thoai muon dung video thu hai va chuyen sang video thu ba
            else if (dialogues[currentDialogueIndex] == "When the light returned, I opened my eyes to find myself lying on a hospital bed, the fluorescent lights shining directly into my eyes. Familiar hospital images surrounded me, and I felt as if I had just emerged from a nightmare.")
            {
                StopVideo3(); // Dung video 3 neu dang phat
                PlayVideo4(); // Phat video 4
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
        SceneManager.LoadScene("Chapter1");
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

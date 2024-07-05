using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    private UiMenuSelection uiManager;
    private UiMenuControl uiControl;
    public SceneLoader sceneLoader;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Tìm và lấy tham chiếu đến UiManager trong Scene
        uiManager = FindObjectOfType<UiMenuSelection>();
        uiControl = FindObjectOfType<UiMenuControl>();

    }

    public void StartGame()
    {
        if (uiManager != null)
        {
            uiManager.SelectionLevel(true); // Kích hoạt màn hình chọn cấp độ khi bấm nút Start
        }
    }
    public void ControlPanel()
    {
        if (uiControl != null)
        {
            uiControl.SelectionLevel(true); // Kích hoạt màn hình control khi bấm nút control
        }
    }

    public void Play()
    {

        NoteUI.ResetNoteCount(); // Reset note count when starting the game
        SceneManager.LoadScene(1);
    }

    public void MainMenuUV()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Map0()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void OnChapter1ButtonClicked()
    {
        sceneLoader.LoadScene("Chapter1");
    }

    public void OnChapter2ButtonClicked()
    {
        sceneLoader.LoadScene("Chapter2");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        // Lấy index của scene hiện tại
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // Nếu index của scene hiện tại là 0 (trường hợp đặc biệt), không làm gì cả
        if (currentIndex == 0)
        {
            Debug.LogWarning("Không thể quay lại scene trước đó vì đây là scene đầu tiên trong build settings.");
            return;
        }

        // Load lại scene trước đó
        SceneManager.LoadScene(currentIndex - 1);
    }
    public void Back()
    {
        if (uiControl != null)
        {
            uiControl.BackToMainMenu();
        }
    }

}

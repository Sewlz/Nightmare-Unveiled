using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f;  // Kho?ng c�ch t��ng t�c
    public LayerMask interactableLayer;  // Layer c?a �?i t�?ng c� th? t��ng t�c
    public string messageWithoutKeyCard = "You need a key card to interact with this monitor.";  // Th�ng �i?p hi?n th? khi kh�ng c� key card
    public string messageWithKeyCard = "Loading data...";  // Th�ng �i?p hi?n th? khi c� key card v� b?t �?u t?i
    public float interactionTime = 5.0f;  // Th?i gian t��ng t�c �? ho�n th�nh qu� tr?nh
    public float idleTimeout = 10.0f;  // Th?i gian ch? tr�?c khi quay tr? l?i tr?ng th�i ch? �?i
    public Slider loadingBar;  // Thanh loading �? hi?n th? ti?n tr?nh
    public TextMeshProUGUI infoText;  // TextMeshPro �? hi?n th? th�ng tin ?n
    public KeyCardInteraction keyCardInteraction;  // Tham chi?u �?n script x? l? key card

    private MonitorController currentMonitor;  // M�n h?nh hi?n t?i �ang t��ng t�c
    private bool isInteracting = false;  // �ang t��ng t�c v?i m�n h?nh hay kh�ng
    private float startTime;  // Th?i gian b?t �?u t��ng t�c
    private float lastInteractionTime;  // Th?i �i?m l?n cu?i t��ng t�c
    private bool interactionCompleted = false;  // ��nh d?u �? ho�n th�nh t��ng t�c
    private bool hasInteractedWithKeyCard = false;  // ��nh d?u �? t��ng t�c v?i key card hay ch�a

    private void Start()
    {
        startTime = -1;  // Kh?i t?o th?i gian b?t �?u t��ng t�c khi b?t �?u
        lastInteractionTime = Time.time;  // Kh?i t?o th?i �i?m l?n cu?i t��ng t�c khi b?t �?u
    }

    private void Update()
    {
        // Ki?m tra xem c� nh?n F �? t��ng t�c v� kh�ng �ang t��ng t�c
        if (Input.GetKeyDown(KeyCode.F) && !isInteracting)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Ki?m tra va ch?m v?i �?i t�?ng c� th? t��ng t�c
            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                MonitorController monitor = hit.collider.GetComponent<MonitorController>();
                if (monitor != null)
                {
                    // Ki?m tra n?u �? c� key card v� ch�a t��ng t�c v?i key card l?n n�o
                    if (keyCardInteraction.HasKeyCard() && !hasInteractedWithKeyCard)
                    {
                        // Thi?t l?p th�ng �i?p v� b?t m�n h?nh
                        monitor.SetMessage(messageWithKeyCard);
                        monitor.ToggleScreen();
                        monitor.SetInteracting(true);

                        // L�u l?i m�n h?nh hi?n t?i �ang t��ng t�c
                        currentMonitor = monitor;
                        isInteracting = true;  // �ang t��ng t�c v?i m�n h?nh
                        startTime = Time.time;  // Ghi nh?n th?i �i?m b?t �?u t��ng t�c

                        // Hi?n th? thanh loading v� th�ng tin t��ng t�c
                        loadingBar.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);
                        hasInteractedWithKeyCard = true;  // ��nh d?u �? t��ng t�c v?i key card
                    }
                    else if (!keyCardInteraction.HasKeyCard())
                    {
                        // Hi?n th? th�ng �i?p khi ch�a c� key card
                        monitor.SetMessage(messageWithoutKeyCard);
                        monitor.ToggleScreen();
                        monitor.SetInteracting(true);

                        // L�u l?i m�n h?nh hi?n t?i �ang t��ng t�c
                        currentMonitor = monitor;
                        isInteracting = true;  // �ang t��ng t�c v?i m�n h?nh

                        lastInteractionTime = Time.time;  // C?p nh?t th?i �i?m l?n cu?i t��ng t�c
                    }
                }
            }
        }

        // N?u �ang t��ng t�c v� c� key card, c?p nh?t thanh loading v� hi?n th? th�ng tin
        if (isInteracting && hasInteractedWithKeyCard)
        {
            float progress = (Time.time - startTime) / interactionTime;
            loadingBar.value = Mathf.Clamp01(progress);  // C?p nh?t gi� tr? thanh loading t? 0 �?n 1

            // Ki?m tra xem �? ho�n th�nh t��ng t�c ch�a
            if (progress >= 1.0f)
            {
                // Ho�n th�nh t��ng t�c, hi?n th? th�ng tin ?n
                infoText.text = "Hidden information revealed!";
                interactionCompleted = true;  // ��nh d?u �? ho�n th�nh t��ng t�c

                // C?p nh?t th?i �i?m k?t th�c t��ng t�c
                startTime = -1;

                // ?n thanh loading khi ho�n th�nh t��ng t�c
                loadingBar.gameObject.SetActive(false);
                infoText.gameObject.SetActive(true);

                // Kh�ng g?i ResetInteraction() ? ��y n?a
                isInteracting = false;  // K?t th�c t��ng t�c v?i m�n h?nh
            }
        }

        // Ki?m tra n?u �ang t��ng t�c v� qu� th?i gian idleTimeout
        if (isInteracting && !hasInteractedWithKeyCard && Time.time - lastInteractionTime > idleTimeout)
        {
            // Ng?ng t��ng t�c v� tr? v? tr?ng th�i ban �?u
            currentMonitor.SetInteracting(false);
            currentMonitor.ToggleScreen();
            isInteracting = false;  // K?t th�c t��ng t�c v?i m�n h?nh
        }
    }

    private void ResetInteraction()
    {
        isInteracting = false;
        interactionCompleted = false;
        currentMonitor.SetInteracting(false);
        currentMonitor.ToggleScreen();
        currentMonitor = null;
    }
}

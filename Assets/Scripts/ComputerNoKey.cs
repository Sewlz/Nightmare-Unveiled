using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerNoKey : MonoBehaviour
{
    public float interactionDistance = 2.0f;  // Kho?ng c�ch t��ng t�c
    public LayerMask interactableLayer;  // L?p c?a �?i t�?ng c� th? t��ng t�c
    public string message = "This is the text to display when pressing F";  // The message to display
    public float idleTimeout = 10.0f;  // Th?i gian ch? tr�?c khi quay tr? l?i tr?ng th�i ch? �?i

    private MonitorController currentMonitor;  // M�n h?nh hi?n t?i �ang t��ng t�c
    private bool isInteracting = false;  // �ang t��ng t�c v?i m�n h?nh hay kh�ng
    private float lastInteractionTime;  // Th?i �i?m l?n cu?i t��ng t�c

    private void Start()
    {
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
                    // Thi?t l?p th�ng �i?p v� b?t m�n h?nh
                    monitor.SetMessage(message);
                    monitor.ToggleScreen();
                    monitor.SetInteracting(true);

                    // L�u l?i m�n h?nh hi?n t?i �ang t��ng t�c
                    currentMonitor = monitor;
                    isInteracting = true;  // �ang t��ng t�c v?i m�n h?nh

                    lastInteractionTime = Time.time;  // C?p nh?t th?i �i?m l?n cu?i t��ng t�c
                }
            }
        }

        // Ki?m tra n?u �ang t��ng t�c v� qu� th?i gian idleTimeout
        if (isInteracting && Time.time - lastInteractionTime > idleTimeout)
        {
            // Ng?ng t��ng t�c v� tr? v? tr?ng th�i ban �?u
            currentMonitor.SetInteracting(false);
            currentMonitor.ToggleScreen();
            isInteracting = false;  // K?t th�c t��ng t�c v?i m�n h?nh
        }
    }
}


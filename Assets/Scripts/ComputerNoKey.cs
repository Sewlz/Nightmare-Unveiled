using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerNoKey : MonoBehaviour
{
    public float interactionDistance = 2.0f;  // Kho?ng cách týõng tác
    public LayerMask interactableLayer;  // L?p c?a ð?i tý?ng có th? týõng tác
    public string message = "This is the text to display when pressing F";  // The message to display
    public float idleTimeout = 10.0f;  // Th?i gian ch? trý?c khi quay tr? l?i tr?ng thái ch? ð?i

    private MonitorController currentMonitor;  // Màn h?nh hi?n t?i ðang týõng tác
    private bool isInteracting = false;  // Ðang týõng tác v?i màn h?nh hay không
    private float lastInteractionTime;  // Th?i ði?m l?n cu?i týõng tác

    private void Start()
    {
        lastInteractionTime = Time.time;  // Kh?i t?o th?i ði?m l?n cu?i týõng tác khi b?t ð?u
    }

    private void Update()
    {
        // Ki?m tra xem có nh?n F ð? týõng tác và không ðang týõng tác
        if (Input.GetKeyDown(KeyCode.F) && !isInteracting)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Ki?m tra va ch?m v?i ð?i tý?ng có th? týõng tác
            if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                MonitorController monitor = hit.collider.GetComponent<MonitorController>();
                if (monitor != null)
                {
                    // Thi?t l?p thông ði?p và b?t màn h?nh
                    monitor.SetMessage(message);
                    monitor.ToggleScreen();
                    monitor.SetInteracting(true);

                    // Lýu l?i màn h?nh hi?n t?i ðang týõng tác
                    currentMonitor = monitor;
                    isInteracting = true;  // Ðang týõng tác v?i màn h?nh

                    lastInteractionTime = Time.time;  // C?p nh?t th?i ði?m l?n cu?i týõng tác
                }
            }
        }

        // Ki?m tra n?u ðang týõng tác và quá th?i gian idleTimeout
        if (isInteracting && Time.time - lastInteractionTime > idleTimeout)
        {
            // Ng?ng týõng tác và tr? v? tr?ng thái ban ð?u
            currentMonitor.SetInteracting(false);
            currentMonitor.ToggleScreen();
            isInteracting = false;  // K?t thúc týõng tác v?i màn h?nh
        }
    }
}


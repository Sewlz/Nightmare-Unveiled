using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerInteraction : MonoBehaviour
{
    public float interactionDistance = 2.0f;  // Kho?ng cách týõng tác
    public LayerMask interactableLayer;  // Layer c?a ð?i tý?ng có th? týõng tác
    public string messageWithoutKeyCard = "You need a key card to interact with this monitor.";  // Thông ði?p hi?n th? khi không có key card
    public string messageWithKeyCard = "Loading data...";  // Thông ði?p hi?n th? khi có key card và b?t ð?u t?i
    public float interactionTime = 5.0f;  // Th?i gian týõng tác ð? hoàn thành quá tr?nh
    public float idleTimeout = 10.0f;  // Th?i gian ch? trý?c khi quay tr? l?i tr?ng thái ch? ð?i
    public Slider loadingBar;  // Thanh loading ð? hi?n th? ti?n tr?nh
    public TextMeshProUGUI infoText;  // TextMeshPro ð? hi?n th? thông tin ?n
    public KeyCardInteraction keyCardInteraction;  // Tham chi?u ð?n script x? l? key card

    private MonitorController currentMonitor;  // Màn h?nh hi?n t?i ðang týõng tác
    private bool isInteracting = false;  // Ðang týõng tác v?i màn h?nh hay không
    private float startTime;  // Th?i gian b?t ð?u týõng tác
    private float lastInteractionTime;  // Th?i ði?m l?n cu?i týõng tác
    private bool interactionCompleted = false;  // Ðánh d?u ð? hoàn thành týõng tác
    private bool hasInteractedWithKeyCard = false;  // Ðánh d?u ð? týõng tác v?i key card hay chýa

    private void Start()
    {
        startTime = -1;  // Kh?i t?o th?i gian b?t ð?u týõng tác khi b?t ð?u
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
                    // Ki?m tra n?u ð? có key card và chýa týõng tác v?i key card l?n nào
                    if (keyCardInteraction.HasKeyCard() && !hasInteractedWithKeyCard)
                    {
                        // Thi?t l?p thông ði?p và b?t màn h?nh
                        monitor.SetMessage(messageWithKeyCard);
                        monitor.ToggleScreen();
                        monitor.SetInteracting(true);

                        // Lýu l?i màn h?nh hi?n t?i ðang týõng tác
                        currentMonitor = monitor;
                        isInteracting = true;  // Ðang týõng tác v?i màn h?nh
                        startTime = Time.time;  // Ghi nh?n th?i ði?m b?t ð?u týõng tác

                        // Hi?n th? thanh loading và thông tin týõng tác
                        loadingBar.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);
                        hasInteractedWithKeyCard = true;  // Ðánh d?u ð? týõng tác v?i key card
                    }
                    else if (!keyCardInteraction.HasKeyCard())
                    {
                        // Hi?n th? thông ði?p khi chýa có key card
                        monitor.SetMessage(messageWithoutKeyCard);
                        monitor.ToggleScreen();
                        monitor.SetInteracting(true);

                        // Lýu l?i màn h?nh hi?n t?i ðang týõng tác
                        currentMonitor = monitor;
                        isInteracting = true;  // Ðang týõng tác v?i màn h?nh

                        lastInteractionTime = Time.time;  // C?p nh?t th?i ði?m l?n cu?i týõng tác
                    }
                }
            }
        }

        // N?u ðang týõng tác và có key card, c?p nh?t thanh loading và hi?n th? thông tin
        if (isInteracting && hasInteractedWithKeyCard)
        {
            float progress = (Time.time - startTime) / interactionTime;
            loadingBar.value = Mathf.Clamp01(progress);  // C?p nh?t giá tr? thanh loading t? 0 ð?n 1

            // Ki?m tra xem ð? hoàn thành týõng tác chýa
            if (progress >= 1.0f)
            {
                // Hoàn thành týõng tác, hi?n th? thông tin ?n
                infoText.text = "Hidden information revealed!";
                interactionCompleted = true;  // Ðánh d?u ð? hoàn thành týõng tác

                // C?p nh?t th?i ði?m k?t thúc týõng tác
                startTime = -1;

                // ?n thanh loading khi hoàn thành týõng tác
                loadingBar.gameObject.SetActive(false);
                infoText.gameObject.SetActive(true);

                // Không g?i ResetInteraction() ? ðây n?a
                isInteracting = false;  // K?t thúc týõng tác v?i màn h?nh
            }
        }

        // Ki?m tra n?u ðang týõng tác và quá th?i gian idleTimeout
        if (isInteracting && !hasInteractedWithKeyCard && Time.time - lastInteractionTime > idleTimeout)
        {
            // Ng?ng týõng tác và tr? v? tr?ng thái ban ð?u
            currentMonitor.SetInteracting(false);
            currentMonitor.ToggleScreen();
            isInteracting = false;  // K?t thúc týõng tác v?i màn h?nh
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

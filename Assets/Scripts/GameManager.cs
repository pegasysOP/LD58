using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;

    public PlayerController playerController;
    public CameraController cameraController;
    public HudController hudController;

    public Inventory inventory;
    public MetalDetector metalDetector;
    public Upgrades upgrades;

    public bool LOCKED = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audioManager.Init();

        inventory = FindFirstObjectByType<Inventory>();
        metalDetector = FindFirstObjectByType<MetalDetector>();
        upgrades = FindFirstObjectByType<Upgrades>();

        SetLocked(false);
    }

    public void DestroySelf()
    {
        Time.timeScale = 1;

        Destroy(gameObject);
    }

    public void SetLocked(bool locked)
    {
        LOCKED = locked;
        Cursor.visible = locked;
        Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
    }
}

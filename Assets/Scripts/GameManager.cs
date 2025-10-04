using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;

    public PlayerController playerController;
    public CameraController cameraController;

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
    }
    public void DestroySelf()
    {
        Time.timeScale = 1;

        Destroy(gameObject);
    }
}

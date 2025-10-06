using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;

    public PlayerController playerController;
    public CameraController cameraController;
    public HudController hudController;

    public Inventory inventory;
    public MetalDetector metalDetector;
    public List<UpgradeData> upgrades = new List<UpgradeData>();

    private Keyboard keyboard;

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
        keyboard = Keyboard.current;
        audioManager.Init();

        inventory = FindFirstObjectByType<Inventory>();
        metalDetector = FindFirstObjectByType<MetalDetector>();

        upgrades.Add(new UpgradeData { upgrade = Upgrade.Battery,           name = "Battery Upgrade",   cost = 1f });
        upgrades.Add(new UpgradeData { upgrade = Upgrade.Range,             name = "Range Upgrade",     cost = 1f });
        upgrades.Add(new UpgradeData { upgrade = Upgrade.Backpack,          name = "Backpack Upgrade",  cost = 1f });
        upgrades.Add(new UpgradeData { upgrade = Upgrade.WalletSize,        name = "Wallet Size",       cost = 1f });
        upgrades.Add(new UpgradeData { upgrade = Upgrade.Rarity,            name = "Rarity Boost",      cost = 1f });
        //upgrades.Add(new UpgradeData { upgrade = Upgrade.GoldDetector,      name = "Gold Detector",     cost = 1f });
        //upgrades.Add(new UpgradeData { upgrade = Upgrade.HeartbeatSensor,   name = "Heartbeat Sensor",  cost = 1f });
        //upgrades.Add(new UpgradeData { upgrade = Upgrade.Fossils,           name = "Fossil Finder",     cost = 1f });
        //upgrades.Add(new UpgradeData { upgrade = Upgrade.AlienTech,         name = "Alien Tech",        cost = 1f });

        hudController.UpdateMoneyText(inventory.GetMoney());
        hudController.UpdateBatteryText(metalDetector.maxBattery);
        hudController.UpdateInventory();

        SetLocked(false);
    }

    private void Update()
    {
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            hudController.pauseMenu.Toggle();
        }
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

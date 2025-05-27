using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int foodConsumption = 3;
    
    [SerializeField] private UIController ui;
    [SerializeField] private BuildMenuController buildMenu;
    [SerializeField] private PlaceBuildingController placeBuildingController;
    [SerializeField] private GameObject villager;

    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    
    public bool gamePaused = false;
    
    private float timerElapsedTime = 0f;
    private bool timerIsRunning = false;
    
    public static GameMode Instance { get; private set; }

    public int initialFoodResource;
    public int initialWoodResource;
    public int initialStoneResource;
    
    private int foodResource;
    private int woodResource;
    private int stoneResource;

    public Transform townHallTransform;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartGame();
        StartTimer();
        foodResource = initialFoodResource;
        woodResource = initialWoodResource;
        stoneResource = initialStoneResource;
        ResourcesChanged();
    }

    void StartGame()
    {
        // Spawn 3 villagers
        Vector3[] offsets =
        {
            new (2, 0, -5),
            new (-2, 0, -5),
            new (0, 0, -5)
        };

        for (int i = 0; i < offsets.Length; i++)
        {
            SpawnVillager(townHallTransform.position + offsets[i]);
        }
        
    }

    public void ConsumeFood()
    {
        foodResource -= foodConsumption;
        if (foodResource <= 0)
        {
            LoseGame();
        }
        ResourcesChanged();
    }

    /// <summary>
    ///  For cheats.
    /// </summary>
    public void ConsumeAllFood()
    {
        foodResource = 0;
        ResourcesChanged();
    }

    public void RemoveWood(int amount)
    {
        woodResource -= amount;
        ResourcesChanged();
    }

    public void RemoveStone(int amount)
    {
        stoneResource -= amount;
        ResourcesChanged();
    }

    public void BringFood(int amount)
    {
        foodResource += amount;
        ResourcesChanged();
    }
    
    public void BringWood(int amount)
    {
        woodResource += amount;
        ResourcesChanged();
    }
    
    public void BringStone(int amount)
    {
        stoneResource += amount;
        ResourcesChanged();
    }

    private void ResourcesChanged()
    {
        ui.UpdateStats(foodResource, woodResource, stoneResource);
        
        bool canBuildHouse = woodResource >= 50 && stoneResource >= 5 ? true : false;
        bool canBuildMonument = woodResource >= 500 && stoneResource >= 200 ? true : false;
        
        buildMenu.SetCardsActive(canBuildHouse, canBuildMonument);
    }

    public void StartBuildingHouse()
    {
        placeBuildingController.StartPlacingHouse();
    }

    public void StartBuildingMonument()
    {
        placeBuildingController.StartPlacingMonument();
    }

    public void SpawnVillager(Vector3 position)
    {
        Instantiate(villager, position, Quaternion.Euler(0f, 180f, 0f));
    }
    
    public void StartTimer()
    {
        timerElapsedTime = 0f;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
        ui.SetTimerText(timerElapsedTime);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timerElapsedTime += Time.deltaTime;
        }
    }

    public void WinGame()
    {
        StopTimer();
        PauseGame();
        WinPanel.SetActive(true);
    }

    private void LoseGame()
    {
        PauseGame();
        LosePanel.SetActive(true);
    }

    private void PauseGame()
    {
        gamePaused = true;
    }
}

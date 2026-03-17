using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BoardManager BoardManager;
    public PlayerController PlayerController;
    public TurnManager TurnManager { get; private set; }

    private int m_FoodAmount = 20;
    public UIDocument UIDoc;
    private Label m_FoodLabel;
    private Label m_LevelLabel;

    private int m_CurrentLevel = 1;

    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        m_LevelLabel = UIDoc.rootVisualElement.Q<Label>("LevelLabel");

        m_GameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverMessage");

        StartNewGame();
    }

    public void StartNewGame()
    {
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        m_CurrentLevel = 1;
        m_FoodAmount = 20;
        m_FoodLabel.text = "Energy: " + m_FoodAmount;
        m_LevelLabel.text = "Level: " + m_CurrentLevel;

        BoardManager.Clean();
        BoardManager.Init();

        PlayerController.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
    }

    public void NewLevel()
    {
        BoardManager.Clean();
        BoardManager.Init();
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
        m_CurrentLevel++;
        m_LevelLabel.text = "Level: " + m_CurrentLevel;
    }

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Energy: " + m_FoodAmount;

        if (m_FoodAmount <= 0)
        {
            PlayerController.GameOver();
            m_GameOverPanel.style.visibility = Visibility.Visible;
            m_GameOverMessage.text = "Game Over!\n\nYou completed " + m_CurrentLevel + " levels\n\nPress enter to restart";

        }
    }

    public void BackMain()
    {
        SceneManager.LoadScene("Start");
    }

    public int EnemyCount()
    {
        return 2 + (m_CurrentLevel - 1) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

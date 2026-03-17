using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
    public Tile EndTile;
    private AudioSource m_AudioSource;
    public AudioClip exitGate;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public override void Init(Vector2Int coord)
    {
        base.Init(coord);
        GameManager.Instance.BoardManager.SetCellTile(coord, EndTile);

    }

    public override void PlayerEntered()
    {
        if (exitGate != null)
        {
            AudioSource.PlayClipAtPoint(exitGate, transform.position);
        }
        GameManager.Instance.NewLevel();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

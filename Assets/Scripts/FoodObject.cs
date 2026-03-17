using UnityEngine;

public class FoodObject : CellObject
{
    public int AmountGranted = 5;
    private AudioSource m_AudioSource;
    public AudioClip collectSound;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public override void PlayerEntered()
    {
        if(collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        GameManager.Instance.ChangeFood(AmountGranted);
        Destroy(gameObject);
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

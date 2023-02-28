using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private ScoreSO scoreSO;
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    private int scoreCanGet = 1;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            audioSource.PlayOneShot(audioClip);
            scoreSO.AddScore(scoreCanGet);
            Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

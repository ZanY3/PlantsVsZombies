using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseController : MonoBehaviour
{
    public GameObject loseCanvas;
    public GameObject bgMusic;
    public AudioClip loseSound;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public async void Lose()
    {
        loseCanvas.SetActive(true);
        bgMusic.SetActive(false);
        source.PlayOneShot(loseSound);
        await new WaitForSeconds(5);
        Time.timeScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Lose();
        }
    }
}

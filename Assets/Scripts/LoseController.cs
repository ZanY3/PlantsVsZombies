using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseController : MonoBehaviour
{
    public void Lose()
    {
        SceneManager.LoadScene("Game");
        //lose things
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            Lose();
        }
    }
}

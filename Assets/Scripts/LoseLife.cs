using UnityEngine;

public class LoseLife : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Life Lost!");
        gameManager.RemoveLife();
        Destroy(collision.gameObject);

    }
  
}

using UnityEngine;

public class Edible : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.SendMessage("Heal");
            Destroy(gameObject);
        }
    }


   
}

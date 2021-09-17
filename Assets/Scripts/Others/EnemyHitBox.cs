using UnityEngine;

public class EnemyHitBox : PlayerHitBox
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioController.instance.PlaySound(sfx[Random.Range(0,1)], 0.5f, Random.Range(0.2f, 0.8f));
            collision.SendMessage("GetSlash", fighter.GetAttackDmg());
        }
    }
}

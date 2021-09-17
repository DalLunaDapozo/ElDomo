using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{

    protected Fighter fighter = null;

    [SerializeField] public AudioClip[] sfx = null;

    private void Awake()
    {
        fighter = GetComponentInParent<Fighter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (WeaponsSwitch.mode == Mode.sword)
                AudioController.instance.PlaySound(sfx[Random.Range(3, 4)], 1.0f, 1.0f);
            
            
            collision.SendMessage("GetSlash", fighter.GetAttackDmg());

            return;
        
        }
        else if (collision.gameObject.tag == "Background")
        {
            if (WeaponsSwitch.mode == Mode.sword)
                AudioController.instance.PlaySound(sfx[Random.Range(5,6)], 1.0f, 1.0f);

            return;
        }
   
          
    
    }
}

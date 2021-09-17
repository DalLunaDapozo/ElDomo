using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    #region VARIABLES
    //MOVEMENT
    [SerializeField] protected float vertical_speed = 0.0f;
    [SerializeField] protected float horizontal_speed = 0.0f;
    
    //STATS
    [SerializeField] protected int maxhealth;

   
    [SerializeField] protected HealthBar healthbar = null;
    [SerializeField] public int currenthealth;

    [SerializeField] public AudioClip[] sfx = null;

    //ANIMATION
    [SerializeField] protected SpriteRenderer spriteRenderer = null;
    [SerializeField] protected Animator animator = null;

    //ATTACK VARIABLES
    [SerializeField] public int attackdamage;


    //PROTECTEDS
    protected BoxCollider2D boxCollider2D = null;
    protected new Rigidbody2D rigidbody = null;

    public bool IsDead = false;

   

    #endregion

    #region UNITY_CALLS
    protected virtual void Start()
    {
        healthbar.SetMaxHealth(maxhealth);
        healthbar.SetHealth(currenthealth);
    }
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();

    }
    #endregion

    #region PROTECTED_FUNCTIONS
    
    protected virtual void GetSlash(int attk)
    {
     
        StartCoroutine("RedFeedback");

        //TAKES DMG
        TakesDamage(attk);

        //SET DMG TO UI
        healthbar.SetHealth(currenthealth);

        //IF ENEMY 0 HP
        OnZeroHP();

      
    }
   
    protected void Heal()
    {
        currenthealth = maxhealth;
        healthbar.SetHealth(maxhealth);
    }
    protected virtual void TakesDamage(int attk)
    {
        animator.SetTrigger("ReceiveDamage");
        currenthealth -= attk;
    }
    protected IEnumerator RedFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = Color.white;
    }
   
    #endregion

    #region PRIVATE_FUNCTIONS
    
    private void OnZeroHP()
    {
        if (currenthealth <= 0)
        {
            IsDead = true;
            StartCoroutine("DeathAnimation");
        }
        else
        {
            return;
        }
    }

   
    protected virtual IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        

        yield return new WaitForSeconds(4f);

        if (gameObject.CompareTag("Enemy"))
        {
            AutoDestroy();
        }
    }

    #endregion

    #region PUBLIC_FUNCTIONS

    public int GetAttackDmg()
    {
        return attackdamage;
    }

    
    public void AutoDestroy()
    {
        Destroy(gameObject);

    }
    #endregion
}

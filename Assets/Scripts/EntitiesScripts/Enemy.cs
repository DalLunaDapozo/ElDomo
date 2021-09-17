using System.Collections;
using UnityEngine;

public class Enemy : Fighter
{
    public enum States { patrol, pursuit }

    #region SERIALIZEFIELDS

    [SerializeField] public States state = States.patrol;

    [SerializeField] protected int expgiven;
    
    [SerializeField] protected float searchRange = 1;
    [SerializeField] protected float stoppingDistance = 0.3f;

   
  

    #endregion

    #region PROTECTED VARIABLES

    protected Transform playerPos; //CHECKS PLAYERS POS


    protected Vector3 target; //MOVES TO THIS POINT   
    protected Vector2 speed; //MOVES?

   
    #endregion

    #region UNITY_CALLS
    protected override void Start()
    {

        base.Start();
        
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if (IsDead == false)
        {
            InvokeRepeating("SetTarget", 0, 2);
            InvokeRepeating("AttackPlayer", 0, 1);
        }

       

    }

    protected virtual void Update()
    {
        if (!DialogueController.DialogueOn)
        {
            if (IsDead == false)
            {
                speed.Normalize();
                speed = target - transform.position;
                FlipSprite();

                OnPersuitState();

                OnPatrolState();

                OnRunningAnimation();

                OnBeenNearPlayer();

                UpdateMovement();
            }
            else
            {

                healthbar.gameObject.SetActive(false);
                Destroy(boxCollider2D);

            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))
        {
            StartCoroutine("StopOnWall");
           
        }
    }

    #endregion

    #region PROTECTED_FUNCTIONS
    protected void OnDrawGizmosSelected()
    {
    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
        Gizmos.DrawWireSphere(target, 0.2f);

    }
    protected void OnPersuitState()
    {
        if (state == States.pursuit && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_death"))
        {
            target = playerPos.transform.position;
            if (Vector3.Distance(target, transform.position) > searchRange * 1.2f)
            {
                target = transform.position;
                state = States.patrol;
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }    
    }
    protected void OnPatrolState()
    {
        if (state == States.patrol)
        {
            var ob = Physics2D.CircleCast(transform.position, searchRange, Vector2.up);
            if (ob.collider != null)
            {
                if (ob.collider.CompareTag("Player"))
                {
                    state = States.pursuit;
                    return;
                }
                else
                {
                    return;
                }
            }
         
        }

    }
    protected void SetTarget()
    {
        if (state != States.patrol)
        {
            return;
        }
        
        target = new Vector2(transform.position.x + Random.Range(-searchRange, searchRange), Random.Range(-10, 5));
    }
    protected virtual void FlipSprite()
    {
        if (speed.x < 0)
        {
            spriteRenderer.transform.localRotation = new Quaternion(0.0f, 180f, 0.0f, 0.0f);
        }
        else if (speed.x > 0)
        {
            spriteRenderer.transform.localRotation = new Quaternion(0.0f, 0f, 0.0f, 0.0f);
        }
    }
    protected void AttackPlayer()
    {
        if (state != States.pursuit)
        {
            return;
        }

        if (speed.magnitude != 0)
        {
            return;
        }

        if (GameController.GameIsOver == false)
        {
            animator.SetTrigger("Attack");
        }

    }
    private void OnRunningAnimation()
    {
        if (rigidbody.velocity.magnitude > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
    protected void UpdateMovement()
    {
        rigidbody.velocity = new Vector2(speed.x * horizontal_speed, speed.y * vertical_speed);
    }
    protected void OnBeenNearPlayer()
    {
        if (speed.magnitude < stoppingDistance)
        {
            speed = Vector2.zero;
        }
        else
        {
            return;
        }
    }



    #endregion

    #region IENUMERATORS

    protected override IEnumerator DeathAnimation()
    {
        AudioController.instance.PlaySound(sfx[0], 1, 1);
        StartCoroutine(base.DeathAnimation());
        yield return new WaitForSeconds(0.1f);
    }
    protected IEnumerator StopOnWall()
    {
        rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.8f);

        target = new Vector2(transform.position.x + Random.Range(-searchRange, searchRange), Random.Range(-2f, 2f));
    }

   
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int enemyLife;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundLayer, playerLayer;

    //patrulha
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Ataque
    public float timeBetweenAttack;
    bool hasAttacked;

    //Estados
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //public TextMeshProUGUI lifeText;

    //public int playerCurrentLife;
    //public int playerLife;


    private void Start()
    {
        //playerLife = GameManager.Instance.life;
        //playerCurrentLife = playerLife;
    }
    public void EnemyTakeDamage(int value)
    {
        enemyLife -= value;

        if (enemyLife <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("AUCH");
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        //Checar se o player ta no alcance
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //Acoes
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Se o walkpoint for alcançado
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculo pra ponto aleatorio
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }   
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!hasAttacked)
        {
            GameManager.Instance.playerCurrentLife -= 2;
            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
            GameManager.Instance.LifeUpdate();

        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, sightRange);
    }

    /*public void LifeUpdate()
    {
        if(playerCurrentLife != playerLife)
        {
            lifeText.text = "vida:" + playerLife;
            playerLife = playerCurrentLife;
            GameManager.Instance.life = playerLife;
            GameManager.Instance.IsDead();
        }
    }*/
}

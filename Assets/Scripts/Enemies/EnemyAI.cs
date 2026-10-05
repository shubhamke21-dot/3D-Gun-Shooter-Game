using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    public float moveSpeed = 2.5f;
    public float chaseRange = 14f;
    public float attackRange = 1.8f;
    public int maxHealth = 50;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    public float patrolRadius = 8f;

    private Transform player;
    private Vector3 patrolTarget;
    private int currentHealth;
    private float nextAttackTime;
    private EnemyState currentState = EnemyState.Patrol;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        patrolTarget = GetRandomPatrolPoint();
    }

    private void Update()
    {
        if (player == null || currentHealth <= 0)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }

        if (currentState == EnemyState.Chase)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;

            if (distance > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 8f);
        }
        else
        {
            if (Vector3.Distance(transform.position, patrolTarget) < 0.6f)
                patrolTarget = GetRandomPatrolPoint();

            Vector3 patrolDirection = (patrolTarget - transform.position).normalized;
            patrolDirection.y = 0f;
            transform.position = Vector3.MoveTowards(transform.position, patrolTarget, moveSpeed * 0.5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(patrolDirection), Time.deltaTime * 5f);
        }
    }

    private Vector3 GetRandomPatrolPoint()
    {
        Vector3 point = transform.position + new Vector3(Random.Range(-patrolRadius, patrolRadius), 0f, Random.Range(-patrolRadius, patrolRadius));
        return point;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        if (player == null) return;

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
            playerController.TakeDamage(attackDamage);
    }
}

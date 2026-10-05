using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2.6f;
    public float chaseRange = 14f;
    public float attackRange = 1.8f;
    public int maxHealth = 50;
    public int attackDamage = 10;
    public float attackCooldown = 1f;

    private Transform target;
    private int currentHealth;
    private float nextAttackTime;

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target == null || currentHealth <= 0)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= chaseRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;

            if (distance > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 6f);
        }
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
        if (target == null) return;

        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }
}

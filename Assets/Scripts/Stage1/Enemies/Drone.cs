using System.Collections;
using UnityEngine;

public class Drone : BaseEnemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fleeRange;

    private bool isAlerting = false;

    protected override void Start()
    {
        base.Start();
        // Begin in Patrolling state if patrol points set. Begin in Idle state otherwise
        currentState = (patrolPoints != null && patrolPoints.Length > 0) ? EnemyState.Patrolling : EnemyState.Idle;
    }

    void Update()
    {
        if (target == null || isDead)
        {
            // Target is dead, do nothing
            return;
        }
        float distance = Vector2.Distance(transform.position, target.position);
        // Enemy not aware
        if (!hasBeenAlerted && !isAlerting)
        {
            // Player within detection range or attacked, switch to Alerted state
            if (distance <= detectionRange || tookDamage)
            {
                ChangeState(EnemyState.Alerted);
            }
        }
        // Enemy aware
        else if (!isAlerting)
        {
            // Enter Fleeing state if in fleeing range, Shooting state if in shooting range; enter Pursuing state otherwise
            if (distance <= fleeRange)
            {
                ChangeState(EnemyState.Fleeing);
            }
            else if (distance <= shootingRange)
            {
                ChangeState(EnemyState.Shooting);
            }
            else
            {
                ChangeState(EnemyState.Pursuing);
            }
        }
        // Execute current behavior
        switch (currentState)
        {
            case EnemyState.Idle:
                anim.Play("Idle");
                break;

            case EnemyState.Patrolling:
                Patrol();
                break;

            case EnemyState.Alerted:
                FaceTarget();
                StartCoroutine(AlertBeforePursuing());
                break;

            case EnemyState.Pursuing:
                FaceTarget();
                PursueTarget();
                break;

            case EnemyState.Shooting:
                FaceTarget();
                ShootAtTarget();
                break;

            case EnemyState.Fleeing:
                FaceAway();
                FleeFromTarget();
                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
        {
            // State unchanged, do nothing
            return;
        }
        currentState = newState;
    }

    IEnumerator AlertBeforePursuing()
    {
        // Pause for alert duration, then switch to Pursuing state
        isAlerting = true;
        hasBeenAlerted = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(alertDuration);
        ChangeState(EnemyState.Pursuing);
        isAlerting = false;
    }

    void PursueTarget()
    {
        if (target == null)
        {
            return;
        }
        // Move towards target, play walking animation
        anim.Play("Walk");
        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void FleeFromTarget()
    {
        if (target == null)
        {
            return;
        }
        // Move away from target, play walking animation
        anim.Play("Walk");
        Vector2 direction = (transform.position - target.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    void ShootAtTarget()
    {
        // Check attack cooldown
        if (Time.time - lastShootTime >= shootCooldown)
        {
            // Attack ready
            anim.Play("Idle");
            lastShootTime = Time.time;
            if (projectilePrefab != null && firePoint != null)
            {
                // Calc attack direction
                Vector2 shootDir = (target.position - firePoint.position).normalized;
                // Rotate bullet sprite to match firing angle
                float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg + 270f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                // Create bullet
                GameObject bullet = Instantiate(projectilePrefab, firePoint.position, rotation);
                // Apply the collision script
                BasicProj bulletScript = bullet.GetComponent<BasicProj>();
                if (bulletScript != null)
                {
                    // Pass on weapon information to script
                    bulletScript.Initialize(shootDir, bulletLifeTime);
                    bulletScript.knockback = shotKnockback;
                    bulletScript.damage = shotDamage;
                    bulletScript.speed = shotSpeed;
                }
            }
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            // No patrol points set, do nothing
            return;
        }
        Transform destination = patrolPoints[currentPatrolIndex];
        Vector2 dir = (destination.position - transform.position);
        anim.Play("Walk");
        if (dir.magnitude < 0.2f)
        {
            // Hit patrol point, move on to next (looping)
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        else
        {
            // Move towards next patrol point (using moveSpeed)
            transform.position += (Vector3)(dir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    public override void Die()
    {
        //Stop all current movement, play death anim
        isDead = true;
        if (rigidBody != null)
        {
            rigidBody.linearVelocity = Vector2.zero;
        }
        anim.Play("Death");
        giveCurrencyToPlayerTarget();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void FaceTarget()
    {
        if (target == null)
        {
            return;
        }
        Vector3 scale = transform.localScale;
        // Flip sprite X based on target position
        if (target.position.x < transform.position.x)
        {
            // Face left
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            // Face right
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void FaceAway()
    {
        if (target == null) return;

        Vector3 scale = transform.localScale;
        // Flip sprite X based on target position
        if (target.position.x < transform.position.x)
        {
            // Face right
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            // Face left
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        // Set gizmos for detection/shooting/fleeing range visualizations
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fleeRange);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Boss : BaseEnemy
{
    private bool isVulnerable = true;


    public override void TakeDamage(int amount)
    {
       if (isVulnerable)
        {
            base.TakeDamage(amount);
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
}

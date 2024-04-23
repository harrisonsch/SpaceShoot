using UnityEngine;

public class CritPoint : MonoBehaviour
{
    public Lvl1Boss boss; // Reference to the main boss script
    public float critMultiplier = 2.0f; // Damage multiplier for critical hits


        public void takeDamage(float damage) {
                boss.Damage(damage * critMultiplier);
        }
}
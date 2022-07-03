using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ManaBreakable : MonoBehaviour
{
    [System.Serializable]
    public class Dropable
    {
        public GameObject item;
        public bool canDrop;
    }

    [SerializeField] private int maxLife = 1;
    [SerializeField] private List<Dropable> rewards = new List<Dropable>();
    private HashSet<Dropable> rewardsHashSet = new HashSet<Dropable>();
    private int currentLife;
    private bool isDestroyed;

    void Awake()
    {
        currentLife = maxLife;
        foreach (var item in rewards)
        {
            rewardsHashSet.Add(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.gameObject.GetComponent<MagicalBullet>();
        if (bullet != null && collision.gameObject.layer == 8)
            TakeDamage(bullet.AttackStats.MagicalDamage);
    }

    private void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            if (currentLife <= 0 && !isDestroyed)
                Die();
        }
    }

    protected void RewardDrop()
    {
        if (rewardsHashSet.Count == 0) return;
        
        GameObject item;

        if(rewardsHashSet.Count == 1)
        {
            if (rewardsHashSet.Single().canDrop)
            {
                item = Instantiate(rewardsHashSet.Single().item);
                item.transform.position = transform.position;
            }
            return;
        }

        var drops = rewardsHashSet.Where(p => p.canDrop == true);
        if (drops.Count() > 1)
        {
            int random = Mathf.RoundToInt(Random.Range(0, drops.Count()));
            item = Instantiate(drops.ElementAt(random).item);
        }
        else
        {
            item = Instantiate(drops.Single().item);
        }

        item.transform.position = transform.position;
    }

    private void Die()
    {
        isDestroyed = true;
        RewardDrop();
        Destroy(gameObject);
    }
}

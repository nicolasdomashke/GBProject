using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private Animator animator;
    void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Debug.Log(this.gameObject.name + " повержен!");
            animator.SetBool("dead", true);
            StartCoroutine(Dying());
        }
    }
    private IEnumerator Dying()
    {
        yield return new WaitForSeconds(.35f);
        GameData.score++;
        Destroy(this.gameObject);
    }
}

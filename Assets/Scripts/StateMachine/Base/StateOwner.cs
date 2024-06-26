using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StateOwner : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;

    public event Action<int> OnHealthChanged;

    protected void Start()
    {
        currentHealth = maxHealth;
    }


    public void Damage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void Dead() { }

    public void ResetHealth() { currentHealth = maxHealth; }
}

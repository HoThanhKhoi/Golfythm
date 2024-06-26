using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StateOwner : MonoBehaviour
{
    [field:SerializeField] public int MaxHealth {  get; set; }
    public int CurrentHealth {  get; set; }

    public event Action<int> OnHealthChanged;

    protected void Start()
    {
        CurrentHealth = MaxHealth;
    }


    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0;
    }

    public void Dead() { }

    public void ResetHealth() { CurrentHealth = MaxHealth; }
}

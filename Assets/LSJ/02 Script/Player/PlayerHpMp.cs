using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpMp : MonoBehaviour
{
    public float CurrentHP { get; private set; }
    public float CurrentMana { get; private set; }

    private void Awake()
    {
        CurrentHP = PlayerStatManager.Instance.MaxHP;
        CurrentMana = PlayerStatManager.Instance.MaxMana;
    }

    private void Update()
    {
        CurrentHP += PlayerStatManager.Instance.HPRegenPerSec * Time.deltaTime;
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, PlayerStatManager.Instance.MaxHP);

        CurrentMana += PlayerStatManager.Instance.ManaRegenPerSec * Time.deltaTime;
        CurrentMana = Mathf.Clamp(CurrentMana, 0f, PlayerStatManager.Instance.MaxMana);
    }

    public void TakeDamage(float amount)
    {
        CurrentHP -= amount;
        CurrentHP = Mathf.Max(0f, CurrentHP);

        if (CurrentHP <= 0)
        {
            // »ç¸Á Ã³¸®
            GetComponent<Player>().ChangeState(GetComponent<Player>().DeadState);
        }

    }

    public bool UseMana(float amount)
    {
        if (CurrentMana >= amount)
        {
            CurrentMana -= amount;
            return true;
        }
        return false;
    }
}

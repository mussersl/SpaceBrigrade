using System.Collections;
using UnityEngine;

[System.Serializable]
public class HeroStats {
    public string name;

    public float baseHP;
    public float currentHP;

    public int strength;
    public int intellect;
    public int dexterity;
    public int constitution;

    public void Damage(float damage)
    {
        currentHP -= (damage * Mathf.Pow(0.95f,constitution));
    }

    public float getAttack()
    {
        return strength;
    }
}

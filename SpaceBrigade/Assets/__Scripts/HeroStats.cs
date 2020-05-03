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

    public bool defending = false;

    public void Damage(float damage)
    {
        float defenseMult = Mathf.Pow(0.95f, constitution);
        currentHP -= (damage * defenseMult);
        if (defending)
        {
            currentHP += 0.5f * (damage * defenseMult);
        }
    }

    public float getAttack()
    {
        return strength;
    }
}

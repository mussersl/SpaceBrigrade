using UnityEngine;
using System.Collections;

 [System.Serializable]
public class BaseEnemy
{
    public string name;

    public enum Type
    {
        CYBORG
    }

    public Type EnemyType;

    public float baseHP;
    public float currentHP;

    public float baseATK;
    public float curATK;

    public float baseDEF;
    public float curDEF;

    public void Damage(float damage)
    {
        currentHP -= (damage - curDEF);
    }
}

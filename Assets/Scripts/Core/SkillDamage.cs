using UnityEngine;
using ItemSystem;

public class SkillDamage : MonoBehaviour
{
    public Skill Sk;
    public float Amount;
    public EnumRef.CharacterType DamageFrom;
    public float DestroyTime;

    // void Awake()
    // {
    //     Destroy(gameObject, this.DestroyTime);
    // }

    // void Update()
    // {
    //     if (this.DestroyTime <= 0)
    //         Destroy(gameObject);
    //     this.DestroyTime -= Time.deltaTime;
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Hit: " + collision.gameObject.name);
    //     // Destroy(gameObject);
    // }
}

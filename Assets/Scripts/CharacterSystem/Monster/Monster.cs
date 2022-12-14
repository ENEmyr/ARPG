using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;
using ItemSystem;

[RequireComponent(typeof(MonsterStats))]
[RequireComponent(typeof(MonsterCombat))]
[RequireComponent(typeof(MonsterProgression))]
public class Monster : MonoBehaviour
{
    #region Singleton
    public static Monster s_Instance;
    void Awake()
    {
        if (s_Instance != null)
        {
            Debug.LogWarning("More than one instance of Player found!");
            return;
        }
        s_Instance = this;
    }
    private Monster() { }
    #endregion

    public MonsterStats Stats;
    public MonsterCombat Combat;
    public MonsterProgression Progression;

    [SerializeField]
    private float fireSpeed = 10;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform dartThrowPoint;
    [SerializeField]
    private bool debug = false;

    private bool shootBool = false;
    private GameObject dartTemp;
    private Rigidbody rb;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.s_Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(Random.value * 10000.0f) % 1000 == 0)
            Attack();
    }

    private void LateUpdate()
    {
        FaceTarget();
    }

    private void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Attack()
    {
        float offSet = .2f;
        Ray raycast = new Ray();
        Skill sk = this.Stats.Skills[0];
        Vector3 firePosition = transform.position;
        firePosition.y *= -offSet;
        firePosition.z *= .4f;
        raycast.origin = transform.position;
        raycast.direction = transform.forward;
        GameObject skill = Instantiate(sk.Renderer, firePosition, transform.rotation);
        SkillDamage script = skill.AddComponent<SkillDamage>();
        script.DamageFrom = EnumRef.CharacterType.Boss;
        script.Amount = this.Combat.Attack(Player.s_Instance.Stats, sk);
        script.Sk = sk;
    }

}

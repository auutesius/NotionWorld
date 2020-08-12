using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Actions;
using NotionWorld.Worlds;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon;
    public string AttackTag;
    [HideInInspector] public Vector3 forward;
    public bool IsSkilling;

    private bool IsColdingDown;
    private Entity entity;
    private Attack attack;
    private AttackAction action;
    private int InternalTime;
    private void Start()
    {
        entity = GetComponent<Entity>();
        attack = entity.GetCapability<Attack>();
        action = new AttackAction();
        gameObject.layer = LayerMask.NameToLayer(AttackTag == "Player" ? "Enemy" : "Player");
        weapon.layer = LayerMask.NameToLayer(AttackTag == "Player" ? "Enemy" : "Player");
    }

    public void SetCollider(bool t)
    {
        weapon.GetComponent<PolygonCollider2D>().enabled = t;
    }

    private void Update()
    {
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        if (IsSkilling)
        {
            return;
        }

        if (CheckAttackTarget() != null)
        {
            Vector3 aimForward = CheckAttackTarget().transform.position - transform.position;
            if (!IsColdingDown)
            {
                StartCoroutine(WeaponCD(aimForward));
            }
            WeaponLookAt(aimForward);
        }
        else
        {
            WeaponLookAt(forward);
        }
    }


    /// <summary>
    /// 使武器指向指定方向
    /// </summary>
    /// <param name="aimingForward"></param>
    public void WeaponLookAt(Vector3 aimingForward)
    {
        Vector3 yDir = weapon.transform.up;
        Vector3 targetDir = new Vector3(aimingForward.x, aimingForward.y, 0f);
        float euler = Vector3.Angle(yDir, targetDir);
        weapon.transform.Rotate(new Vector3(0f, 0f, Vector3.Cross(yDir, targetDir).z > 0 ? euler : -euler));
    }

    /// <summary>
    /// 检测最近的Boss与敌人进行预瞄
    /// </summary>
    /// <returns></returns>
    public GameObject CheckAttackTarget()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attack.Range);
        if (cols.Length > 0)
        {
            List<float> distanceList = new List<float>();
            Dictionary<GameObject, float> colsDic = new Dictionary<GameObject, float>();
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].transform.CompareTag(AttackTag))
                {
                    float distance = (cols[i].transform.position - transform.position).magnitude;
                    if (!colsDic.ContainsKey(cols[i].gameObject))
                    {
                        colsDic.Add(cols[i].gameObject, distance);
                    }
                    else
                    {
                        colsDic[cols[i].gameObject] = distance;
                    }
                    if (!distanceList.Contains(distance))
                    {
                        distanceList.Add(distance);
                    }
                }
            }
            if (distanceList.Count > 0)
            {
                distanceList.Sort();
                foreach (KeyValuePair<GameObject, float> c in colsDic)
                {
                    if (c.Value == distanceList[0]) { return c.Key; }
                }
                return null;
            }
            else { return null; }
        }
        else { return null; }
    }

    IEnumerator WeaponCD(Vector3 aimForward)
    {
        IsColdingDown = true;
        action.AttackDir = aimForward;
        action.AttackTag = AttackTag;
        action.TakeAction(entity);
        yield return new WaitForSeconds(attack.Interval);
        IsColdingDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            return;
        }
        if (AttackTag != null && AttackTag != "")
        {
            if (collision.gameObject.CompareTag(AttackTag))
            {
                HealthModifier healthModifier = new HealthModifier(-25);    //角色定值技能()写死
                MoveTowardFragment moveTowardFragment = new MoveTowardFragment();

                moveTowardFragment.Direction = ((collision.transform.position - transform.position).normalized);
                moveTowardFragment.InternalTime = 0.5f;
                moveTowardFragment.Speed = 0.4f;
                moveTowardFragment.TakeEffect(collision.GetComponent<Entity>());

                /*
                GravitationModifier gravitationModifier = new GravitationModifier(transform.position, 0.2f,1);
                healthModifier.TakeEffect(collision.gameObject.GetComponent<Entity>());
                gravitationModifier.Center = (collision.transform.position - transform.position).normalized + collision.transform.position;
                gravitationModifier.TakeEffect(collision.transform.gameObject.GetComponent<Entity>());
                */
            }
        }
    }
}

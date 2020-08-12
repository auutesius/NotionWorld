using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class ShotgunSabot : SkillBullet
{
    public float speed;

    public float time;

    public string buckshot;

    public float angle;

    public int count;

    public int wave;

    public float waveInterval;

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        transform.forward = direction;
        StartCoroutine(MoveCorotinue());
    }

    private IEnumerator MoveCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;

        Vector2 deltaForward = transform.forward * speed * Time.fixedDeltaTime;
        Vector2 position = transform.position;

        while (timer > 0)
        {
            transform.position = position;
            position += deltaForward;
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }

        WaitForSeconds interval = new WaitForSeconds(waveInterval);
        for (int i = 0; i < wave; i++)
        {
            CreateBarrage();
            yield return interval;
        }

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void CreateBarrage()
    {
        float deltaAngle = angle / (count - 1);
        transform.Rotate(-angle / 2, 0, 0);

        for (int i = 0; i < count; i++)
        {
            var bullet = ObjectPool.GetObject(buckshot, "SkillBullets");
            SkillBullet skill = bullet.GetComponent<SkillBullet>();
            skill.Source = Source;
            skill.Target = Target;
            skill.Launch(transform.position, transform.forward);

            transform.Rotate(deltaAngle, 0, 0);
        }
    }
}

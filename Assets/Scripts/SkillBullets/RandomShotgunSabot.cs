using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class RandomShotgunSabot : SkillBullet
{
    public float speed;

    public float time;

    public string buckshot;

    public float minDeltaAngle;

    public float maxDeltaAngle;

    public int count;

    public int wave;

    public float waveInterval;

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        transform.right = direction;
        StartCoroutine(MoveCorotinue());
    }

    private IEnumerator MoveCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;

        Vector2 movement = transform.right * speed * Time.fixedDeltaTime;
        Vector2 position = transform.position;

        while (timer > 0)
        {
            transform.position = position;
            position += movement;
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }

        for (int i = 0; i < wave; i++)
        {
            CreateBarrage();
            timer = waveInterval;

            while (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                yield return wait;
            }
        }

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void CreateBarrage()
    {
        float deltaAngle = Random.Range(minDeltaAngle, maxDeltaAngle);
        transform.Rotate(0, 0, -deltaAngle * count / 2);

        for (int i = 0; i < count; i++)
        {
            var bullet = ObjectPool.GetObject(buckshot, "SkillBullets");
            SkillBullet skill = bullet.GetComponent<SkillBullet>();
            skill.Source = Source;
            skill.Target = Target;

            skill.Launch(transform.position, transform.right);

            transform.Rotate(0, 0, deltaAngle);
            deltaAngle = Random.Range(minDeltaAngle, maxDeltaAngle);
        }
    }
}

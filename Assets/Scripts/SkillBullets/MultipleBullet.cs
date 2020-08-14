using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class MultipleBullet : SkillBullet
{
    public string[] bullets;

    public float interval;

    private void Awake()
    {
        var intervals = interval * bullets.Length;
        coldDown = coldDown > intervals ? coldDown : intervals;
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        transform.right = direction;
        StartCoroutine(ShootCorotinue());
    }

    private IEnumerator ShootCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer;

        for (int i = 0; i < bullets.Length; i++)
        {
            Vector2 direction = (Target.transform.position - transform.position).normalized;
            transform.right = direction;

            GameObject prefab = NotionWorld.Worlds.ObjectPool.GetObject(bullets[i], "SkillBullets");
            SkillBullet bullet = prefab.GetComponent<SkillBullet>();
            bullet.Source = Source;
            bullet.Target = Target;

            timer = interval;
            while (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                yield return wait;
            }
            bullet.Launch(transform.position, transform.right);
        }

        ObjectPool.RecycleObject(this.gameObject);
    }

}

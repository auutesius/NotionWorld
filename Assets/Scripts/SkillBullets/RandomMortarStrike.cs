using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public class RandomMortarStrike : SkillBullet
{
    public Rect area;

    public int count;

    public string mortar;

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;

        for (int i = 0; i < count; i++)
        {
            var obj = ObjectPool.GetObject(mortar, "SkillBullets");
            SkillBullet skill = obj.GetComponent<SkillBullet>();
            skill.Source = Source;
            skill.Target = Target;

            Vector2 pos = new Vector2(Random.Range(area.xMin, area.xMax), Random.Range(area.yMin, area.yMax));

            skill.Launch(pos, transform.right);
        }
    }
}

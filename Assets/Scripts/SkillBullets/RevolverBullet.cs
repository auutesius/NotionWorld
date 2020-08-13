using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;
using NotionWorld.Actions;

public sealed class RevolverBullet : SkillBullet
{
    public int damage;
    public float speed;
    public float time;
    [Tooltip("撞击的特效名")] public string HitEffectName;

    private HealthModifier healthModifier;
    private AnimatorTriggerModifier animatorTrigger;
    private AudioPlayModifier audioPlayModifier;
    private CreatEffectModifier creatEffectModifier;
    private AudioSource audioSource;

    private void Awake()
    {
        healthModifier = new HealthModifier()
        {
            DeltaValue = -damage
        };
        animatorTrigger = new AnimatorTriggerModifier()
        {
            Name = "Hit"
        };
        audioPlayModifier = new AudioPlayModifier()
        {
            Audio = gameObject.GetComponent<AudioSource>()
        };
        creatEffectModifier = new CreatEffectModifier()
        {
            EffectName = HitEffectName
        };
    }

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

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var gameObject = other.gameObject;

        if (gameObject == Target)
        {
            var entity = gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                healthModifier.Health = entity.GetCapability<Health>();
            }
            animatorTrigger.Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

            animatorTrigger.Name = "Hit";
            if (other.gameObject.GetComponent<Entity>().GetCapability<Health>().Value < 0)
            {
                animatorTrigger.Name = "Die";

                if (other.gameObject.CompareTag("Player"))
                {
                    StopPlayerMovementFragment stopPlayerMovementFragment = new StopPlayerMovementFragment(0f);
                    stopPlayerMovementFragment.TakeEffect(other.GetComponent<Entity>());

                    StopPlayerStateFragment stopPlayerStateFragment = new StopPlayerStateFragment(0f);
                    stopPlayerStateFragment.TakeEffect(other.GetComponent<Entity>());
                };
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            creatEffectModifier.HitPoint = transform;
            TakeEffect();

            ObjectPool.RecycleObject(this.gameObject);
        }
    }

    private void TakeEffect()
    {
        healthModifier.TakeEffect();
        animatorTrigger.TakeEffect();
        audioPlayModifier.TakeEffect();
        creatEffectModifier.TakeEffect();
    }
}

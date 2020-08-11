using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace NotionWorld.Actions
{
    public sealed class InvincibleFragment : Fragment
    {
        public float InternalTime;
        public override void TakeEffect(Entity actor)
        {
            Debug.Log(actor.GetComponent<PolygonCollider2D>().enabled);
            actor.GetComponent<PolygonCollider2D>().enabled = false;
            Delay(InternalTime * 1000, actor);
        }
        private async void Delay(float ms, Entity actor)
        {
            Debug.Log(actor.GetComponent<PolygonCollider2D>().enabled);
            await Task.Delay((int)ms);
            actor.GetComponent<PolygonCollider2D>().enabled = true;

            Debug.Log(actor.GetComponent<PolygonCollider2D>().enabled);
        }
    }

}

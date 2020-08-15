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
            //Debug.Log(actor.GetComponent<PolygonCollider2D>().enabled);
            //actor.GetComponent<PolygonCollider2D>().enabled = false;
            actor.transform.GetChild(2).gameObject.SetActive(true);
            actor.transform.GetChild(2).GetComponent<Animator>().Play("muteki");
            Delay(InternalTime * 1000, actor);
        }
        private async void Delay(float ms, Entity actor)
        {
            await Task.Delay((int)ms);
            //actor.GetComponent<PolygonCollider2D>().enabled = true;
            actor.transform.GetChild(2).gameObject.SetActive(false);
            //Debug.Log(actor.GetComponent<PolygonCollider2D>().enabled);
            //Debug.Log(actor.transform.GetChild(2).gameObject.active);
        }
    }

}

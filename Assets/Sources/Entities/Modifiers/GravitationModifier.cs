using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using System.Threading.Tasks;

namespace NotionWorld.Entities
{
    public sealed class GravitationModifier : Modifier
    {
        public float GravitationInternal;
        public float GravitationPower;
        // 引力中心
        public Vector3 Center
        {
            get; set;
        }

        public GravitationModifier(Vector3 center, float skillInternal, float power)
        {
            Center = center;
            GravitationInternal = skillInternal;
            GravitationPower = power;
        }

        public override void TakeEffect(Entity entity)
        {
            Delay(50, entity);
        }


        private async void Delay(int ms, Entity actor)
        {
            int step = 0;
            
            while (step < GravitationInternal * 1000 / ms)
            {
                await Task.Delay(ms);
                Vector2 perDistance = (Center - actor.transform.position) / (GravitationInternal * 1000 / ms);
                actor.transform.Translate(perDistance * GravitationPower);
                step++;
            }
        }
    }

}
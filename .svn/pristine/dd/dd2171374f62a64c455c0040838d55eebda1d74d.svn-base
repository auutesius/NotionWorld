
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;

namespace NotionWorld.Actions
{
    public sealed class MoveFragment : Fragment
    {
        public Vector3 Movement
        {
            get; set;
        }


        public override void TakeEffect(Entity entity)
        {
            entity.transform.Translate(Movement);
        }
    }
}
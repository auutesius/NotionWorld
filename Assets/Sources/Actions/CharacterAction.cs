using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;

namespace NotionWorld.Actions
{
    public abstract class EntityAction
    {
        public abstract void TakeAction(Entity entity);
    }
}

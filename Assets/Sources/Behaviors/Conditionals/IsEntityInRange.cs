using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;

namespace NotionWorld.Behaivors
{
    public class IsEntityInRange : Conditional
    { 
        [Tooltip("The archetype of entity want to check in range")]
        public EntityArchetype archetype;

        [Tooltip("range radius")]
        public float radius;

        private bool enteredTrigger = false;

        public override void OnEnd()
        {
            enteredTrigger = false;
        }

        public override void OnFixedUpdate()
        {
            
        }
    }

}
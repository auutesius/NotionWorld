using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System;

namespace NotionWorld.Actions
{
    public sealed class AnimatorDirFragment : Fragment
    {
        public Vector3 Movement
        {
            get; set;
        }


        public override void TakeEffect(Entity entity)
        {
            Animator animator = entity.transform.GetChild(0).GetComponent<Animator>();
            if (animator == null) { return; }
            // { throw new InvalidOperationException("There is no Animator on Image Sprite."); }
            if (animator.name != "Image") { return;  }
            // { throw new InvalidOperationException("Get wrong animator, please comfirm the image is the second child."); }
            Vector3 s = animator.transform.localScale;
            animator.transform.localScale = new Vector3(Mathf.Abs(s.x) * (Movement.x > 0 ? 1f : -1f), s.y, s.z);
        }
    }
}
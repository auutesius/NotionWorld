using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

namespace NotionWorld.Actions
{
    public sealed class AudioCreateFragment : Fragment
    {
        
        public float PlayInternal; // 延迟播放间隔
        public string AudioName;
        public override void TakeEffect(Entity actor)
        {
            if (AudioName == null)
            {
                throw new ArgumentNullException("Parameter name is null.");
            }
            Delay(PlayInternal, actor);            
        }
        private async void Delay(float ms, Entity actor)
        {
            await Task.Delay((int)(PlayInternal * 1000));
            GameObject audioObj = ObjectPool.GetObject(AudioName, "Audios");
            if (actor!= null)
            {
                audioObj.transform.position = actor.transform.position;
            }
        }
    }

}
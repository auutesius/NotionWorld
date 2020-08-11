using System;

namespace NotionWorld.Events
{
    public class  SecondUpdateEventArgs : EventArgs
    {
        public float DeltaTime
        {
            get; set;
        }

        public SecondUpdateEventArgs(float time)
        {
            DeltaTime = time;
        }
    }

}
using UnityEngine;

namespace Events
{
    public class FaithEvent : Event
    {
        public override void StartEvent()
        {
            MonkManager.instance.terreur = true;
        }

        public override void EndEvent()
        {
            MonkManager.instance.terreur = false;
        }
    }
}

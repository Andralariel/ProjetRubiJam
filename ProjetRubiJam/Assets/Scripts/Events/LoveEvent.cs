using UnityEngine;

namespace Events
{
    public class LoveEvent : Event
    {
        public override void StartEvent()
        {
            MonkManager.instance.insurrection = true;
        }

        public override void EndEvent()
        {
            MonkManager.instance.insurrection = false;
        }
    }
}

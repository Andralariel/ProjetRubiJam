using UnityEngine;

namespace Events
{
    public class LoveEvent : Event
    {
        public override void StartEvent()
        {
            Debug.Log("Start of LoveEvent");
        }

        public override void EndEvent()
        {
            Debug.Log("End of LoveEvent");
        }
    }
}

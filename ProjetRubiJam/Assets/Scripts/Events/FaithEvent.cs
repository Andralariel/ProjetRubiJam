using UnityEngine;

namespace Events
{
    public class FaithEvent : Event
    {
        public override void StartEvent()
        {
            Debug.Log("Start of FaithEvent");
        }

        public override void EndEvent()
        {
            Debug.Log("End of FaithEvent");
        }
    }
}

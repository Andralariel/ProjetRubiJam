using UnityEngine;

namespace Events
{
    public class MoneyEvent : Event
    {
        public override void StartEvent()
        {
            Debug.Log("Start of MoneyEvent");
        }

        public override void EndEvent()
        {
            Debug.Log("End of MoneyEvent");
        }
    }
}

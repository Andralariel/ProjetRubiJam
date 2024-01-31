using UnityEngine;

namespace Events
{
    public class MoneyEvent : Event
    {
        public override void StartEvent()
        {
            MonkManager.instance.famine = true;
        }

        public override void EndEvent()
        {
            MonkManager.instance.famine = false;
        }
    }
}

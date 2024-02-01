using System.Collections;
using UnityEngine;

namespace Events
{
    public class MoneyEvent : Event
    {
        public override void StartEvent()
        {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            var manager = MonkManager.instance;
            manager.famine = true;
            manager.imageForEvents.sprite = manager.famineSprite;
            var color = manager.imageForEvents.color;
            manager.imageForEvents.color = new Color(color.r, color.g, color.b, 1);
            yield return new WaitForSecondsRealtime(3f);
            manager.imageForEvents.color = new Color(color.r, color.g, color.b, 0);
        }
        
        public override void EndEvent()
        {
            MonkManager.instance.famine = false;
        }
    }
}

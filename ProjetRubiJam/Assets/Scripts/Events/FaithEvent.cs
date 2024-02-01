using System.Collections;
using UnityEngine;

namespace Events
{
    public class FaithEvent : Event
    {
        
        public override void StartEvent()
        {
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            var manager = MonkManager.instance;
            manager.terreur = true;
            manager.imageForEvents.sprite = manager.terreurSprite;
            var color = manager.imageForEvents.color;
            manager.imageForEvents.color = new Color(color.r, color.g, color.b, 1);
            yield return new WaitForSecondsRealtime(3f);
            manager.imageForEvents.color = new Color(color.r, color.g, color.b, 0);
        }

        public override void EndEvent()
        {
            MonkManager.instance.terreur = false;
        }
    }
}

using DG.Tweening;
using UnityEngine;

namespace Events
{
    public class LoveEvent : Event
    {
        public override void StartEvent()
        {
            var manager = MonkManager.instance;
            manager.insurrection = true;
            manager.imageForEvents.sprite = manager.insurrectionSprite;
            manager.imageForEvents.DOFade(1, 3).OnComplete(()=>manager.imageForEvents.DOFade(0, 3));
        }

        public override void EndEvent()
        {
            MonkManager.instance.insurrection = false;
        }
    }
}

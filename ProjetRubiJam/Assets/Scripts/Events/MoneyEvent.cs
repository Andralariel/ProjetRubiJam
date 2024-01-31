using DG.Tweening;

namespace Events
{
    public class MoneyEvent : Event
    {
        public override void StartEvent()
        {
            var manager = MonkManager.instance;
            manager.famine = true;
            manager.imageForEvents.sprite = manager.famineSprite;
            manager.imageForEvents.DOFade(1, 1).OnComplete(()=>manager.imageForEvents.DOFade(0, 5));
        }

        public override void EndEvent()
        {
            MonkManager.instance.famine = false;
        }
    }
}

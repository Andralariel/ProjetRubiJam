using DG.Tweening;

namespace Events
{
    public class FaithEvent : Event
    {
        public override void StartEvent()
        {
            var manager = MonkManager.instance;
            manager.terreur = true;
            manager.imageForEvents.sprite = manager.terreurSprite;
            manager.imageForEvents.DOFade(1, 3).OnComplete(()=>manager.imageForEvents.DOFade(0, 3));
        }

        public override void EndEvent()
        {
            MonkManager.instance.terreur = false;
        }
    }
}

using UnityEngine;

namespace Events
{
    public abstract class Event : MonoBehaviour
    {
        public string textToShow;
    
        public abstract void StartEvent();

        public abstract void EndEvent();
    }
}

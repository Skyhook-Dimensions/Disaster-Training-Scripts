using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{
    public class EventListener : EventListener<Empty>
    { }

    public abstract class EventListener<T> : MonoBehaviour
    {
        [Header("Event Listener Fields")]
        [SerializeField] private EventChannel<T> _eventChannel;
        [SerializeField] protected UnityEvent<T> _unityEvent;

        protected virtual void Awake()
        {
            _eventChannel.Register(this);
        }

        protected virtual void OnDestroy()
        {
            _eventChannel.Unregister(this);
        }

        public void Raise(T value)
        {
            _unityEvent?.Invoke(value);
        }
    }
}
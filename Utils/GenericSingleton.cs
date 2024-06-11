using UnityEngine;

namespace Utils
{
    public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;
        public static bool HasInstance => instance != null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        var obj = new GameObject
                        {
                            name = typeof(T).Name + " Auto-Instantiated",
                        };
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            InstantiateSingleton();
        }

        protected virtual void InstantiateSingleton()
        {
            if (!Application.isPlaying) return;

            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;

// not a good practice in general but allows for lightning fast prototyping
namespace General
{
    public abstract class SingletonMonoBehaviour<TInstance> : MonoBehaviour
        where TInstance : MonoBehaviour
    {
        public static TInstance Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance)
            {
                Debug.LogError($"An instance of {typeof(TInstance).Name} already exists!");
                return;
            }

            if (this is TInstance instance)
            {
                Instance = instance;
            }
        }
    }
}
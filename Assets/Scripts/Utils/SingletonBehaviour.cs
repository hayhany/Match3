using UnityEngine;

namespace Utils
{

    public class SingletonBehaviour<T> : MonoBehaviour
    {
        public static T Instance
        {
            get => _instance;
            private set { _instance = value; }
        }
        private static T _instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this.GetComponent<T>();
            else
                Destroy(this);
        }
    }
}

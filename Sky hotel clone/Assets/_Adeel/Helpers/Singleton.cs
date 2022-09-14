using UnityEngine;

namespace _Adeel.Helpers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Ins
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
                instance = this as T;
            else if (instance != this)
                DestroySelf();
        }

        protected void OnValidate()
        {
            if (instance == null)
                instance = this as T;
            else if (instance != this)
            {
                Debug.Log("Singleton<" + this.GetType() + "> already has an instance on scene. Component will be destroyed.");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.delayCall -= DestroySelf;
                UnityEditor.EditorApplication.delayCall += DestroySelf;
#endif
            }
        }


        private void DestroySelf()
        {
            if (Application.isPlaying)
                Destroy(this);
            else
                DestroyImmediate(this);
        }
    }
}

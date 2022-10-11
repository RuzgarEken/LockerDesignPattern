using Generics.Behaviours;
using UnityEngine;

namespace Generics.Scripts.Behaviours
{

    public class SingletonComponent<T> : ComponentBase
        where T : ComponentBase
    {

        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.Log($"There is no {typeof(T).Name} in scene");
                    }
                }

                return _instance;
            }
        }

    }

}


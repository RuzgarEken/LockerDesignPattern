//using Essentials.Packages.LogSystem;
using UnityEngine;

namespace Essentials.Utilities
{

    public class SingletonBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        //protected static Log Log = new Log(nameof(T), DebugLevels.Debug);

        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null)
                    {
                        //Log.Debug($"There is no {typeof(T).Name} in scene", null, LogTypes.GameLoop);
                    }
                }

                return _instance;
            }
        }

    }

}
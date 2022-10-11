using UnityEditor;
using UnityEngine;

namespace Essentials.Utilities
{

    public class SingletonScriptableObject<T> : ScriptableObject
        where T : ScriptableObject
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
#if UNITY_EDITOR
                    string guid = AssetDatabase.FindAssets($"t:{typeof(T).Name}")[0];
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    _instance = AssetDatabase.LoadAssetAtPath<T>(path);
#else
                    _instance = Resources.FindObjectsOfTypeAll<T>()[0];
#endif

                }

                return _instance;
            }
        }

        protected virtual void OnEnable()
        {
            if(Instance != null && Instance != this)
            {
#if UNITY_EDITOR
                Debug.Log($"<color=cyan> Can not create asset. <color=yellow>{typeof(T).Name}</color> is a singleton scriptable object</color>");
                string assetPath = AssetDatabase.GetAssetPath(this);
                AssetDatabase.DeleteAsset(assetPath);
                AssetDatabase.Refresh();
#endif
            }
        }

    }

}
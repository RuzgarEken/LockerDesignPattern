using UnityEngine;
using Essentials.Utilities;

namespace Generics.Editor
{

    [System.Serializable] public class AssetDictionary : SerializableDictionary<string, Object> { }

    [CreateAssetMenu(menuName = "Generics/Editor/Asset References")]
    public class AssetReferences : SingletonScriptableObject<AssetReferences>
    {
        [SerializeField] private AssetDictionary Assets;

        public T GetAssetInternal<T>(string id)
            where T : Object
        {
            return Assets[id] as T;
        }

        public static T GetAsset<T>(string id)
            where T : Object
        {
            return Instance.GetAssetInternal<T>(id);
        }

    }

}
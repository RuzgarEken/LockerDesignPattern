using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Essentials.Extensions
{

    public static class TransformExtensions
    {

        public static Transform[] GetChildren(this Transform transform)
        {
            Transform[] children = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }

            return children;
        }

        public static bool GetComponentInFirstDegreeChildren<T>(this Transform transform, out T result)
            where T : Component
        {
            var children = GetChildren(transform);

            foreach (var child in children)
            {
                if(child.TryGetComponent<T>(out result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }

        #region Editor

#if UNITY_EDITOR

        public static void AddChildren(this Transform parent, Transform[] newChildren)
        {
            for (int i = 0; i < newChildren.Length; i++)
            {
                newChildren[i].SetParent(parent);
            }
        }

        /// <summary>
        /// Creates parent object for passed transforms. Returns parent transform
        /// </summary>
        public static Transform CreateParentForTransforms(Transform[] children, bool putParentToChildrenOrigin = true, bool createSeperateParents = false)
        {
            Undo.RecordObjects(children, "Create Parent For Transform");

            if (!createSeperateParents)
            {
                Transform childrenLastParent = children[0].parent;
                Transform parent = new GameObject("Parent").transform;
                parent.parent = childrenLastParent;

                int childCounter = 0;
                foreach (Transform child in children)
                {
                    if (childCounter++ == 0)
                        parent.SetSiblingIndex(child.GetSiblingIndex());

                    child.parent = parent;
                }

                if (putParentToChildrenOrigin)
                    PutParentToChildrenOrigin(parent);

                Selection.activeTransform = parent;

                return parent;
            }
            else
            {
                for (int i = 0; i < children.Length; i++)
                {
                    Transform parent = new GameObject("Parent").transform;
                    parent.parent = children[i].parent;
                    children[i].parent = parent;

                    if (putParentToChildrenOrigin)
                        PutParentToChildrenOrigin(parent);
                }
            }

            return null;
        }

        public static void RemoveParent(Transform[] parents)
        {
            for (int i = 0; i < parents.Length; i++)
            {
                Transform newParent = parents[i].parent;
                Transform[] oldParentChildren = parents[i].GetChildren();
                newParent.AddChildren(oldParentChildren);

                int childCounter = 0;
                for (int j = 0; j < oldParentChildren.Length; j++)
                {
                    oldParentChildren[j].SetSiblingIndex(
                            parents[i].GetSiblingIndex() + 1
                    );
                }

                Selection.objects = oldParentChildren.Select(t => t.gameObject).ToArray();
                Object.DestroyImmediate(parents[i].gameObject);
            }
        }

        public static void PutParentToChildrenOrigin(Transform parent)
        {
            Undo.RecordObjects(parent.GetChildren(), "Put Parent To Children Origin");

            List<Transform> children = new List<Transform>();

            Bounds bounds = default;

            while (parent.childCount > 0)
            {
                Transform child = parent.GetChild(0);
                child.parent = null;
                children.Add(child);

                if(bounds == default)
                {
                    bounds = new Bounds(child.position, Vector3.zero);
                }
                else if(!bounds.Contains(child.position))
                {
                    bounds.Encapsulate(child.position);
                }
            }

            if (bounds != default)
            {
                parent.position = bounds.center;
            }

            for (int i = 0; i < children.Count; i++)
            {
                children[i].SetParent(parent);
            }
        }

        public static void IncreaseSiblingIndexes(Transform[] transforms)
        {
            Transform[] sorted = transforms.SortBySiblingIndex();

            for (int i = sorted.Length - 1; i >= 0; i--)
            {
                sorted[i].SetSiblingIndex(sorted[i].GetSiblingIndex() + 1);
            }
        }

        public static void DecreaseSiblingIndex(Transform[] transforms)
        {
            Transform[] sorted = transforms.SortBySiblingIndex();

            for (int i = 0; i < sorted.Length; i++)
            {
                sorted[i].SetSiblingIndex(sorted[i].GetSiblingIndex() - 1);
            }
        }

        public static Transform[] SortBySiblingIndex(this Transform[] transforms)
        {
            return transforms.OrderBy(t => t.GetSiblingIndex()).ToArray();
        }

        public static Transform GetTransformBySiblingIndex(int index, Transform parent = null)
        {
            if (parent == null)
            {
                GameObject[] transforms = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                return transforms.Where(t => t.transform.GetSiblingIndex() == index).Select(t => t.transform).First();
            }
            else
            {
                Transform[] transforms = parent.GetChildren();
                return transforms.Where(t => t.transform.GetSiblingIndex() == index).Select(t => t.transform).First();
            }

        }

#endif

        #endregion

    }

}
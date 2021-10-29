using UnityEngine;

namespace ZDef.Core
{
    public static class TransformExtension
    {
        public static void LocalIdentity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}
using System;
using System.Collections.Generic;
using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class Utils
    {
        public static bool In(int a, int v, int b)
        {
            return v >= a && v < b;
        }

        public static bool In(float a, float v, float b)
        {
            return v >= a && v < b;
        }

        public static int CircleCase(float i, float j, float radius, float range)
        {
            float factor = Mathf.Sqrt(2f) / 2f;
            float halfRange = range / 2;
            // 顺时针
            // 上
            if (In(-halfRange, i, halfRange) &&
                In(-halfRange, j - radius, halfRange))
            {
                return 1;
            }

            if (In(-halfRange, i - radius * factor, halfRange) &&
                In(-halfRange, j - radius * factor, halfRange))
            {
                return 2;
            }

            // 右
            if (In(-halfRange, i - radius, halfRange) &&
                In(-halfRange, j, halfRange))
            {
                return 3;
            }

            if (In(-halfRange, i - radius * factor, halfRange) &&
                In(-halfRange, j + radius * factor, halfRange))
            {
                return 4;
            }

            // 下
            if (In(-halfRange, i, halfRange) &&
                In(-halfRange, j + radius, halfRange))
            {
                return 5;
            }

            if (In(-halfRange, i + radius * factor, halfRange) &&
                In(-halfRange, j + radius * factor, halfRange))
            {
                return 6;
            }

            // 左
            if (In(-halfRange, i + radius, halfRange) &&
                In(-halfRange, j, halfRange))
            {
                return 7;
            }

            if (In(-halfRange, i + radius * factor, halfRange) &&
                In(-halfRange, j - radius * factor, halfRange))
            {
                return 8;
            }

            return -1;
        }

        public static int SquareCase(float i, float j, float radius, float range)
        {
            float halfRange = range / 2;
            // 顺时针
            // 上
            if (In(-halfRange, i, halfRange) &&
                In(-halfRange, j - radius, halfRange))
            {
                return 1;
            }

            if (In(-halfRange, i - radius, halfRange) &&
                In(-halfRange, j - radius, halfRange))
            {
                return 2;
            }

            // 右
            if (In(-halfRange, i - radius, halfRange) &&
                In(-halfRange, j, halfRange))
            {
                return 3;
            }

            if (In(-halfRange, i - radius, halfRange) &&
                In(-halfRange, j + radius, halfRange))
            {
                return 4;
            }

            // 下
            if (In(-halfRange, i, halfRange) &&
                In(-halfRange, j + radius, halfRange))
            {
                return 5;
            }

            if (In(-halfRange, i + radius, halfRange) &&
                In(-halfRange, j + radius, halfRange))
            {
                return 6;
            }

            // 左
            if (In(-halfRange, i + radius, halfRange) &&
                In(-halfRange, j, halfRange))
            {
                return 7;
            }

            if (In(-halfRange, i + radius, halfRange) &&
                In(-halfRange, j - radius, halfRange))
            {
                return 8;
            }

            return -1;
        }
    }

    public class MonoUtils : MonoBehaviour
    {
        public static GM GetGM()
        {
            return GameObject.Find("GM").GetComponent<GM>();
        }
        
        public static T SafeDestroy<T>(T obj) where T : UnityEngine.Object
        {
            if (Application.isEditor)
                UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(obj); };
            else
                Destroy(obj);

            return null;
        }

        public static T SafeDestroyGameObject<T>(T component) where T : Component
        {
            if (component != null)
                SafeDestroy(component.gameObject);
            return null;
        }
    }
}
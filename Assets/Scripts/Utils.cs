using UnityEngine;

public class Utils : MonoBehaviour
{
    public static bool In(int a, int v, int b)
    {
        return v >= a && v < b;
    }

    public static int CircleCase(int i, int j, float radius, int range)
    {
        float factor = Mathf.Sqrt(2f) / 2f;
        int halfRange = range / 2;
        // 顺时针
        // 上
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j - (int) radius, halfRange))
        {
            return 1;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 2;
        }

        // 右
        if (In(-halfRange, i - (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 3;
        }

        if (In(-halfRange, i - (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 4;
        }

        // 下
        if (In(-halfRange, i, halfRange) &&
            In(-halfRange, j + (int) radius, halfRange))
        {
            return 5;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j + (int) (radius * factor), halfRange))
        {
            return 6;
        }

        // 左
        if (In(-halfRange, i + (int) radius, halfRange) &&
            In(-halfRange, j, halfRange))
        {
            return 7;
        }

        if (In(-halfRange, i + (int) (radius * factor), halfRange) &&
            In(-halfRange, j - (int) (radius * factor), halfRange))
        {
            return 8;
        }

        return 0;
    }
}
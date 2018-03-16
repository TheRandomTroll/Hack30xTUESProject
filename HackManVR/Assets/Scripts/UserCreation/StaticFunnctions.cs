using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFunctions
{
    /// <summary>
    /// The miminal predefined workspace coords.
    /// </summary>
    public static Vector3 minimum = new Vector3(-27, 0, -27);

    /// <summary>
    /// The maximal predefined workspace coords.
    /// </summary>
    public static Vector3 maximum = new Vector3(27, 54, 27);

    /// <summary>
    /// Detects wether the specified Vector3 is within the predefined workspace bounds.
    /// </summary>
    public static bool IsWithinBounds(this Vector3 V3)
    {
        return V3.x >= minimum.x && V3.x <= maximum.x && V3.y >= minimum.y && V3.y <= maximum.y && V3.z >= minimum.z && V3.z <= maximum.z;
    }

    /// <summary>
    /// Trims the value to a certain amount of decimals, when not sending a decimalCount, it'll faster to just straight up use Mathf.Round ().
    /// </summary>
    public static float Trim(this float value, int decimalCount = 0)
    {
        float multiplier = Mathf.Pow(10, decimalCount);
        return Mathf.Round(value * multiplier) / multiplier;
    }

    /// <summary>
    /// Executes Mathf.Round() onto a Vector3, with an additional decimal count specifier.
    /// </summary>
    public static Vector3 Round(this Vector3 V3, int decimalCount = 0)
    {
        return new Vector3(V3.x.Trim(decimalCount), V3.y.Trim(decimalCount), V3.z.Trim(decimalCount));
    }

    /// <summary>
    /// Inverts the Vector3 to its positive or negative counterpart, the same result as Mirror (Vector3.zero).
    /// </summary>
    public static Vector3 Invert(this Vector3 V3)
    {
        return new Vector3(-V3.x, -V3.y, -V3.z);
    }

    /// <summary>
    /// Inverts the Vector3 to its positive or negative counterpart, with adjustable axis points to retract from.
    /// </summary>
    public static Vector3 Mirror(this Vector3 V3, Vector3 mirrorPoint)
    {
        return mirrorPoint + (mirrorPoint - V3);
    }

    /// <summary>
    /// Mirrors the value over a given middle point.
    /// </summary>
    public static float Mirror(this float value, float mirrorPoint)
    {
        return mirrorPoint + (mirrorPoint - value);
    }

    /// <summary>
    /// Inverts the Vector3's X axis to its positive or negative counterpart, with an adjustable axis point to retract from.
    /// </summary>
    public static Vector3 MirrorX(this Vector3 V3, float mirrorPoint)
    {
        return new Vector3(V3.x.Mirror(mirrorPoint), V3.y, V3.z);
    }

    /// <summary>
    /// Executes a safe division for calculations with uninitialized values.
    /// </summary>
    public static float DivideBy(this float value, float divisionValue)
    {
        return divisionValue != 0 ? value / divisionValue : 0;
    }

    /// <summary>
    /// Centers the Vector3 to a point on a half unit grid, it has an accuracy of 10 ^ 4.
    /// </summary>
    public static Vector3 SnapToGrid(this Vector3 V3)
    {
        if (Mathf.Abs(V3.x % 1).Trim(4) != 0.5)
            V3.x = Mathf.Round(V3.x);

        if (Mathf.Abs(V3.y % 1).Trim(4) != 0.5)
            V3.y = Mathf.Round(V3.y);

        if (Mathf.Abs(V3.z % 1).Trim(4) != 0.5)
            V3.z = Mathf.Round(V3.z);

        return V3;
    }
}
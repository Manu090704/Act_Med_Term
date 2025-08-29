using UnityEngine;

public class Util{

    public static Quaternion LookAt2D(Vector2 from, Vector2 to)
    {
        Vector2 diff = to - from;
        diff.Normalize();

        float roz_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f,roz_z - 90);
    }

}

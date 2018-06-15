namespace Polyworks
{
    using System.Collections;
    using UnityEngine;

    public class Utils {
        public static bool RandomInRange(float threshold, float x = 0, float y = 100)
        {
            float gen = Random.Range(x, y);

            if(gen <= threshold)
            {
                return true;
            }
            return false;
        }
    }
}
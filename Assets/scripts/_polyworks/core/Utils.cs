namespace Polyworks
{
    using System.Collections;
    using UnityEngine;


    public class Utils {
        public static bool GetIsRandomUnderThreshold(float threshold, float x = 0, float y = 100f)
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
namespace Polyworks 
{
    using UnityEngine;

    public class PolyBehaviour: MonoBehaviour 
    {
        public bool isLogOn;

        public virtual void Log(string message)
        {
            if(isLogOn)
            {
                Debug.Log(message);
            }
        }
    }
}
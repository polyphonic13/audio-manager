namespace Polyworks {
	using UnityEngine;
	public class Singleton<T>: PolyBehaviour where T: PolyBehaviour {
		protected static T instance;
		
		public static T Instance {
			get {
				if(instance == null) {
					instance = (T) FindObjectOfType(typeof(T));
					
					if(instance == null) {
						// Debug.LogError("An instance of " + typeof(T) + " is needed in scene, but not found");
					}
				}
				return instance;
			}
		}
	}
}
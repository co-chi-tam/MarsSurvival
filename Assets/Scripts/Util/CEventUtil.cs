using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.Events.Utils {
	public class CEventUtil {

		[Serializable]
		public class UnityEventBool: UnityEvent<bool> {}

		[Serializable]
		public class UnityEventInt: UnityEvent<int> {}

		[Serializable]
		public class UnityEventFloat: UnityEvent<float> {}

		[Serializable]
		public class UnityEventString: UnityEvent<string> {}

	}
}

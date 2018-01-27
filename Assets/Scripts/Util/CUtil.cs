using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CUtil {

	public static Vector3 ToV3(this string value) {
		value = value.Replace ("(", string.Empty);
		value = value.Replace (")", string.Empty);
		var split = value.Split (',');
		if (split.Length != 3)
			return Vector3.zero;
		return new Vector3 (
			float.Parse (split [0].ToString()),
			float.Parse (split [1].ToString()),
			float.Parse (split [2].ToString()));
	}

}

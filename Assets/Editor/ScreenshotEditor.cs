using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenshotEditor {

	[MenuItem("Util/ScreenShot")]
	public static void CreateScreenShot ()
	{
		ScreenCapture.CaptureScreenshot ("Screenshot-"+ System.DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ssss") + ".png");
	}

}

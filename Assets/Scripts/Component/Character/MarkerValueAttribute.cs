using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, 
	AllowMultiple = true)]
public class MarkerValueAttribute: Attribute {

	public string valueName {
		get; 
		set;
	}

	public string updateMethod { 
		get;
		set;
	}

	public object updateValuePerSecond {
		get;
		set;
	}

	public MarkerValueAttribute ()
	{
		this.valueName 		= "Empty name";
		this.updateMethod 	= "None";
		this.updateValuePerSecond = 0f;
	}

	public override string ToString ()
	{
		return string.Format ("[MarkerValueAttribute: valueName={0}, updateMethod={1}, updateValuePerSecond={2}]", valueName, updateMethod, updateValuePerSecond);
	}

}



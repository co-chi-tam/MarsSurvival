using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UpdateValuePerSecondAttribute: Attribute {

	public string updateMethod { 
		get;
		set;
	}

	public object updateValuePerSecond {
		get;
		set;
	}

	public UpdateValuePerSecondAttribute () : base ()
	{
		this.updateMethod 	= "None";
		this.updateValuePerSecond = null;
	}

	public override string ToString ()
	{
		return string.Format ("[UpdateValuePerSecondAttribute: updateMethod={0}, updateValuePerSecond={1}]", updateMethod, updateValuePerSecond);
	}

}



using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UpdateValuePerInvokeAttribute : Attribute {

	public string updateName {
		get; 
		set;
	}

	public string updateMethod { 
		get;
		set;
	}

	public object updateValuePerInvoke {
		get;
		set;
	}

	public UpdateValuePerInvokeAttribute () : base ()
	{
		this.updateName		= "Empty name";
		this.updateMethod 	= "None";
		this.updateValuePerInvoke = null;
	}

	public override string ToString ()
	{
		return string.Format ("[UpdateValuePerSecondAttribute: valueName={0}, updateMethod={1}, updateValuePerInvoke={2}]", updateName, updateMethod, updateValuePerInvoke);
	}

}

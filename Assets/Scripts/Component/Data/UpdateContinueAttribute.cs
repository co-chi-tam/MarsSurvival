using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UpdateContinueAttribute : Attribute {

	public UpdateContinueAttribute () : base ()
	{
		
	}

	public override string ToString ()
	{
		return string.Format ("[UpdateContinue]");
	}

}

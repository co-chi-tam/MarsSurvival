using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class InfoAttribute : Attribute {

	public string valueName {
		get; 
		set;
	}

	public object valueMin { 
		get;
		set;
	}

	public object valueMax {
		get;
		set;
	}

	public InfoAttribute ()
	{
		this.valueName 		= "Empty name";
		this.valueMin 		= null;
		this.valueMax 		= null;
	}

	public override string ToString ()
	{
		return string.Format ("[InfoAttribute: valueName={0}, valueMin={1}, valueMax={2}]", valueName, valueMin, valueMax);
	}

}

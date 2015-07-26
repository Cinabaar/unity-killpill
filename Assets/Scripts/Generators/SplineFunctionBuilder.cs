using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SplineFunction : IFunction {

	public List<IFunction> Splines = new List<IFunction> ();

	public SplineFunction AddSpline(IFunction spline) {
		if (Splines.Count == 0 || 
		    (Splines.Count != 0 && spline.Start () == Splines [Splines.Count - 1].End ())) {
			Splines.Add (spline);
			return this;
		}
		else
			throw new ArgumentException ();
	}

	public float GetValue(float time) {
		foreach (var entry in Splines) {
			if(entry.Start() <= time && time < entry.End()) {
				return entry.GetValue(time);
			}
		}
		return 0;
	}
	public float Start()
	{
		if(Splines.Count!=0)
			return Splines [0].Start ();
		return 0;
	}
	
	public float End()
	{
		if(Splines.Count!=0)
			return Splines [Splines.Count-1].End ();
		return 0;
	}
}


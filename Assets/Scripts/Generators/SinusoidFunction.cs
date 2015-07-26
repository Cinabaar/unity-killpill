using System;
using UnityEngine;

public class SinusoidFunction : IFunction {

	public float freq;
	public float amp;
	public float offsetX;
	public float offsetY;
	public float start;
	public float end;

	public SinusoidFunction(float freq, float amp, float offsetX, float offsetY, float start, float end) {
		this.freq = freq;
		this.amp = amp;
		this.offsetX = offsetX;
		this.offsetY = offsetY;
	}

	public float GetValue(float time) {
		return amp * Mathf.Sin(freq * time + offsetX) + offsetY;
	}

	public float Start()
	{
		return start;
	}
	
	public float End()
	{
		return end;
	}
}
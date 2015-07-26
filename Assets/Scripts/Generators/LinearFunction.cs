using System;

using UnityEngine;

public class LinearFunction : IFunction
{
	public Vector2 from;
	public Vector2 to;

	public LinearFunction (Vector2 from, Vector2 to) {
		this.from = from;
		this.to = to;
	}

	public float GetValue(float time) {
		return (to.y - from.y) * (time - Start()) / (to.x - from.x) + from.y;
	}

	public float Start()
	{
		return from.x;
	}

	public float End()
	{
		return to.x;
	}
}


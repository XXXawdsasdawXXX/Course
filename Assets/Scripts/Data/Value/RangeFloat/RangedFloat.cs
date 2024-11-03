﻿using System;
using Random = UnityEngine.Random;

namespace Data.RangeFloat
{
	[Serializable]
	public struct RangedFloat
	{
		public float MinValue;
		public float MaxValue;

		public readonly float GetRandomValue()
		{
			return Random.Range(MinValue, MaxValue);
		}

		public bool Contains(float value)
		{
			return MinValue <= value && MaxValue >= value;
		}
	}
}
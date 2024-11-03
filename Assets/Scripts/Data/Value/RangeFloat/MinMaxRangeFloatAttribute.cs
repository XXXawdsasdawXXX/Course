﻿using System;

namespace Data.Value.RangeFloat
{
	public class MinMaxRangeFloatAttribute : Attribute
	{
		public MinMaxRangeFloatAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}
		public float Min { get; private set; }
		public float Max { get; private set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kochonennet
{
	public class VectorRGB
	{
		public int r, g, b;

		public static Random random = new Random();
		public VectorRGB()
		{
			r = 0;
			g = 0;
			b = 0;
		}

		public VectorRGB(int r, int g, int b)
		{
			this.r = r;
			this.b = b;
			this.g = g;
		}

		public VectorRGB(double r, double g, double b)
		{
			var temp = r;
			if (temp > 1) temp = 1;
			if (temp <0 ) temp = 0;
			this.r = (int)(temp * 255d);

			temp = g;
			if (temp > 1) temp = 1;
			if (temp < 0) temp = 0;
			this.g = (int)(temp * 255d);

			temp = b;
			if (temp > 1) temp = 1;
			if (temp < 0) temp = 0;
			this.b = (int)(temp * 255d);
		}

		public VectorRGB(int rand)
		{
			this.r = (int)(rand * random.NextDouble());
			this.g = (int)(rand * random.NextDouble());
			this.b = (int)(rand * random.NextDouble());
		}

		public Color ToColor()
		{
			return Color.FromArgb(r, g, b);
		}
	}
}

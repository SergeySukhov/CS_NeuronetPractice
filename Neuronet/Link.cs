using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronet
{
	public class Link
	{
		public Neuron inNeuron = null;
		public Neuron outNeuron = null;
		public double w = 0.5d;
		public double dw = 0d;
		private static Random rw = new Random();

		public Link()
		{

		}

		public Link(Neuron inN, Neuron outN)
		{
			w = (rw.NextDouble() - 0.5) / 3d + 0.5;
			inNeuron = inN;
			outNeuron = outN;
			inN.inLinks.Add(this);
			outN.outLinks.Add(this);

		}

	}
}

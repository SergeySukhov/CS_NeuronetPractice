using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronet
{
	public class Layer
	{
		public List<Neuron> neurons;

		public Layer()
		{

		}

		public Layer(int layerLength)
		{
			neurons = new List<Neuron>();
			for (int i = 0; i < layerLength; i++)
			{
				neurons.Add(new Neuron());
			}
		}

		// todo: remove binding from class
		/*
		public static void OnetoOneBinding(Layer outLayer, Layer inLayer)
		{
			// exceptions

			for (int i = 0; i < outLayer.neurons.Count; i++)
			{
				Link l = new Link(inLayer.neurons[i], outLayer.neurons[i]);
				outLayer.neurons[i].outLinks.Add(l);
				inLayer.neurons[i].inLinks.Add(l);
			}
		}

		public static void AlltoAllBinding(Layer outLayer, Layer inLayer)
		{
			foreach (var s in outLayer.neurons)
			{
				foreach (var q in inLayer.neurons)
				{
					Link link = new Link(q, s);
					s.outLinks.Add(link);
					q.inLinks.Add(link);
				}
			}
		}
		public static void RandBinding(Layer outLayer, Layer inLayer, double proc)
		{
			Random a = new Random();
			foreach (var s in outLayer.neurons)
			{
				foreach (var q in inLayer.neurons)
				{
					var r = a.NextDouble();
					if (r > proc / 100d) continue;
					Link link = new Link(q, s);
					s.outLinks.Add(link);
					q.inLinks.Add(link);
				}
			}
		}
		*/
	}
}

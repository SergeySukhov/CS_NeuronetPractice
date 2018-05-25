using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neuronet;

namespace Standartnet
{
	public class StandartNet : NeuroNet
	{
		//todo: make code more pretty

		public StandartNet(int inputLength, int hiddenLength, int hiddenWidth, int outputLength)
		{
			InputLayer = new Layer(inputLength);

			HiddenLayers = new List<Layer>();
			for (int i = 0; i < hiddenWidth; i++)
			{
				HiddenLayers.Add(new Layer(hiddenLength));
			}

			OutputLayer = new Layer(outputLength);

			BindClassicNet();

		}

		public StandartNet(int inputLength, int hiddenLength, int hiddenWidth, int outputLength, bool rand)
		{
			InputLayer = new Layer(inputLength);

			HiddenLayers = new List<Layer>();
			for (int i = 0; i < hiddenWidth; i++)
			{
				HiddenLayers.Add(new Layer(hiddenLength));
			}

			OutputLayer = new Layer(outputLength);

			BindRandHidden();

		}

		public StandartNet(int inputLength, int hiddenLength, int hiddenWidth, int outputLength, int test)
		{
			InputLayer = new Layer(inputLength);

			HiddenLayers = new List<Layer>();
			for (int i = 0; i < hiddenWidth; i++)
			{
				HiddenLayers.Add(new Layer(hiddenLength));
			}

			OutputLayer = new Layer(outputLength);

			BindReccNet();

		}

		private static void AlltoAllBinding(Layer outLayer, Layer inLayer)
		{
			foreach (var s in outLayer.neurons)
			{
				foreach (var q in inLayer.neurons)
				{
					Link link = new Link(q, s);
					
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

		private void BindReccNet()
		{
			AlltoAllBinding(InputLayer, HiddenLayers[0]);

			if (HiddenLayers.Count > 1)
				for (int i = 0; i < HiddenLayers.Count - 1; i++)
				{
					for (int j = i + 1; j < HiddenLayers.Count; j++)
						AlltoAllBinding(HiddenLayers[i], HiddenLayers[j]);
				}

			AlltoAllBinding(HiddenLayers.Last(), OutputLayer);
		}

		private void BindRandHidden()
		{
			AlltoAllBinding(InputLayer, HiddenLayers[0]);

			if (HiddenLayers.Count > 1)
				for (int i = 0; i < HiddenLayers.Count - 1; i++)
				{
					RandBinding(HiddenLayers[i], HiddenLayers[i + 1], 60);
				}

			AlltoAllBinding(HiddenLayers.Last(), OutputLayer);
		}

		private void BindClassicNet()
		{
			AlltoAllBinding(InputLayer, HiddenLayers[0]);

			if (HiddenLayers.Count > 1)
				for (int i = 0; i < HiddenLayers.Count - 1; i++)
				{
					AlltoAllBinding(HiddenLayers[i], HiddenLayers[i + 1]);
				}

			AlltoAllBinding(HiddenLayers.Last(), OutputLayer);
		}

		public void StandartStudy(double correctAnswer, double koef)
		{
			

			double sum = 0;
			foreach (var s in OutputLayer.neurons[0].inLinks) sum += s.outNeuron.imp * s.w;

			OutputLayer.neurons[0].imp = (-OutputLayer.neurons[0].imp + correctAnswer) * OutputLayer.neurons[0].tgdx(sum);

			foreach (var s in OutputLayer.neurons[0].inLinks)
			{
				s.dw = OutputLayer.neurons[0].imp * koef * s.outNeuron.imp;
			}

			

			double sigma = 0;
			for (int i = HiddenLayers.Count - 1; i >= 0; i--)
				foreach (var n in HiddenLayers[i].neurons)
				{
					sum = 0;
					sigma = 0;
					foreach (var s in n.inLinks) sum += s.outNeuron.imp * s.w;
					

					foreach (var s in n.outLinks) sigma += s.inNeuron.imp * s.w; //!

					n.imp = sigma * n.tgdx(sum);

					foreach (var s in n.inLinks) s.dw = n.imp * koef * s.outNeuron.imp;

				}

			

			foreach (var l in HiddenLayers)
				foreach (var n in l.neurons)
					foreach (var s in n.inLinks)
					{
						s.w += s.dw;
						s.dw = 0;
					}

			
			foreach (var n in OutputLayer.neurons)
				foreach (var s in n.inLinks)
				{
					s.w += s.dw;
					s.dw = 0;
				}

			

		}

		
	}
}

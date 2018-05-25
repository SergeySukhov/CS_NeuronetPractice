using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neuronet;

namespace Kochonennet
{
    public class KochonenNet : NeuroNet
    {
		public KochonenNet(int inputLength, int hiddenLength)
		{
			InputLayer = new Layer(inputLength);

			HiddenLayers = new List<Layer>();

			HiddenLayers.Add(new Layer(hiddenLength));

			OutputLayer = new Layer(hiddenLength);

			BindKochonenNet();

		}

		public KochonenNet(int inputLength, int hiddenLength, int hiddenWidth)
		{
			InputLayer = new Layer(inputLength);

			HiddenLayers = new List<Layer>();

			for(int i=0; i<hiddenWidth; i++)
			HiddenLayers.Add(new Layer(hiddenLength));

			OutputLayer = new Layer(hiddenLength);

			BindKochonenNetRect();
		}

		private void BindKochonenNetRect() // !
		{
			foreach(var s in HiddenLayers)
			{
				AlltoAllBinding(InputLayer, s);
			}

			//for(int i=0; i<HiddenLayers.Count; i++)
			//	for(int j=0; j<HiddenLayers[i].neurons.Count; j++)
			//	{
			//		Link link = null;
			//		if (j< HiddenLayers[i].neurons.Count-1)
			//			link = new Link(HiddenLayers[i].neurons[j], HiddenLayers[i].neurons[j + 1]);


			//		if (j >0 )
			//			link = new Link(HiddenLayers[i].neurons[j], HiddenLayers[i].neurons[j - 1]);

			//		if (i < HiddenLayers.Count - 1)
			//			link = new Link(HiddenLayers[i].neurons[j], HiddenLayers[i+1].neurons[j ]);

			//		if (i>0)
			//			link = new Link(HiddenLayers[i].neurons[j], HiddenLayers[i-1].neurons[j ]);


			//	}

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

		private void BindKochonenNet()
		{
			AlltoAllBinding(InputLayer, HiddenLayers[0]);

			if (HiddenLayers.Count > 1)
				for (int i = 0; i < HiddenLayers.Count - 1; i++)
				{
					AlltoAllBinding(HiddenLayers[i], HiddenLayers[i + 1]);
				}

			
			OnetoOneBinding(HiddenLayers.Last(), OutputLayer);

			double koordQ = InputLayer.neurons.Count;
			Random a = new Random();

			foreach (var s in HiddenLayers[0].neurons)
			{
				foreach (var inl in s.inLinks) inl.w = 0.5 + (a.NextDouble() - 0.5) / koordQ;
				foreach (var outl in s.outLinks) outl.w = 1d;
			}

		}

		//public override void RunIteration() // итерация для однослойной сети Кохонена, возвращает в выходной слой расстояние вектора(?)
		//{		

		//	for (int i = 0; i < HiddenLayers[0].neurons.Count; i++)
		//	{
		//		HiddenLayers[0].neurons[i].SimpleSumImp();

		//		OutputLayer.neurons[i].imp = HiddenLayers[0].neurons[i].imp;
		//	}

		//}
		
		private Random rnd = new Random();

		private double tg(double nImp)
		{
			//return 1.0 / (1.0 + Math.Pow(Math.E, -nImp));
			return 2.0 / (1.0 + Math.Pow(Math.E, -nImp)) - 1d;
		}


		public void Study(double koef, out int winnerNumi, out int winnerNumj)
		{
			winnerNumi = 0;
			winnerNumj = 0;

			double min = 10000000d, R = 0;

			for(int i=0; i< HiddenLayers.Count; i++)
				for(int j=0; j<HiddenLayers[i].neurons.Count; j++)
				{
					for (int k = 0; k < HiddenLayers[0].neurons[j].inLinks.Count; k++)
					{
						R = 0;
						R += //Math.Abs(HiddenLayers[0].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[0].neurons[j].inLinks[k].w);
							 Math.Pow(HiddenLayers[i].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[i].neurons[j].inLinks[k].w, 2d);
					}

					R = Math.Sqrt(R) / HiddenLayers[i].neurons[j].inLinks.Count;

					if (R < min) 
					{
						min = R;
						winnerNumi = i;
						winnerNumj = j;
					}

					//HiddenLayers[i].neurons[j].imp = R;
					//foreach (var s in HiddenLayers[i].neurons[j].inLinks)
					//{
					//	s.w += (s.outNeuron.imp - s.w)*tg(R);

					//}



				}

			foreach(var s in HiddenLayers[winnerNumi].neurons[winnerNumj].inLinks)
			{
				s.w += koef * (s.outNeuron.imp - s.w);

			}

			
			
			
		}

		public bool Study(double koef, out int winnerNumi, out int winnerNumj, double addingKoef)
		{
			winnerNumi = 0;
			winnerNumj = 0;

			double min = 10000000d, R = 0;

			for (int i = 0; i < HiddenLayers.Count; i++)
				for (int j = 0; j < HiddenLayers[i].neurons.Count; j++)
				{
					for (int k = 0; k < HiddenLayers[0].neurons[j].inLinks.Count; k++)
					{
						R = 0;
						R += //Math.Abs(HiddenLayers[0].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[0].neurons[j].inLinks[k].w);
							 Math.Pow(HiddenLayers[i].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[i].neurons[j].inLinks[k].w, 2d);
					}

					R = Math.Sqrt(R) / HiddenLayers[0].neurons[j].inLinks.Count;

					if (R < min)
					{
						min = R;
						winnerNumi = i;
						winnerNumj = j;
					}	

					

				}


			if (min> addingKoef)
			{
				Neuron insertNeu = new Neuron();

				HiddenLayers[winnerNumi].neurons.Insert(winnerNumj, insertNeu);

				foreach(var s in InputLayer.neurons)
				{
					Link l = new Link(insertNeu, s);
					l.w = s.imp;
				}

				//BindKochonenNetRect();
				return true;
			}

			foreach (var s in HiddenLayers[winnerNumi].neurons[winnerNumj].inLinks)
			{
				s.w += koef * (s.outNeuron.imp - s.w);

			}

			return false;


		}

		public int StudySimple(double koef) // для самообучающейся однослойной сети сети 
		{

			int i = 0;
			double min = 10000000d,	R = 0;

			for (int j = 0; j < HiddenLayers[0].neurons.Count; j++)
			{
				
				R = 0;
				for (int k = 0; k < HiddenLayers[0].neurons[j].inLinks.Count; k++)
				{
					R += //Math.Abs(HiddenLayers[0].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[0].neurons[j].inLinks[k].w);
					     Math.Pow(HiddenLayers[0].neurons[j].inLinks[k].outNeuron.imp - HiddenLayers[0].neurons[j].inLinks[k].w, 2d);

				}

				R = Math.Sqrt(R) / HiddenLayers[0].neurons[j].inLinks.Count;

				if (R < min) //(OutputLayer.neurons[j].imp < min)
				{
					min = R;//OutputLayer.neurons[j].imp;
					i = j;
				}
			}

			for (int j = 0; j < HiddenLayers[0].neurons[i].inLinks.Count; j++)
			{
				HiddenLayers[0].neurons[i].inLinks[j].w += koef * (InputLayer.neurons[j].imp - HiddenLayers[0].neurons[i].inLinks[j].w);
			}

			// имеем самоорганизационный случай, следовательно после разбиения векторов необходимо будет определить те множествва где прогноз является положительным
			// * для задания корректируемых множеств следует при попадании вектора в нужное множество уменьшать связи (расстояния) при не попадании - увеличивать


			return i;
		}


	}
}

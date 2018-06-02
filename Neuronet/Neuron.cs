using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuronet
{
	public class Neuron
	{
		public double imp = 0;

		public List<Link> inLinks = new List<Link>(); // входящие связи
		public List<Link> outLinks = new List<Link>(); // исходящие связи

		public double tg(double nImp)
		{
			//return 1.0 / (1.0 + Math.Pow(Math.E, -nImp));
			return 2.0 / (1.0 + Math.Pow(Math.E, -nImp)) - 1d;
		}

		public double tgdx(double nImp)
		{
			//return tg(nImp) * (1.0 - tg(nImp));
			return (tg(nImp) + 1d) * (1.0 - tg(nImp)) * 0.5;
		}

		public void sumImp() //*
		{
			foreach (var s in inLinks)
			{
				imp += s.outNeuron.imp * s.w;
			}
			imp = tg(imp);
		}

		public double SimpleBackSum()
		{
			double sum = 0;
			foreach (var s in outLinks) sum += s.inNeuron.imp * s.w;
			return sum;
		}

		public void backSum(double koef)
		{
			double sigma = 0, resImp = imp;
					

			foreach (var s in outLinks)
			{
				sigma = tgdx(s.inNeuron.imp) * s.w * imp * koef;
				s.w -= sigma;//!
							 //imp = sigma;

			}

			
		}

		public void SimpleSumImp()
		{
			foreach (var s in inLinks)
			{
				imp += s.outNeuron.imp * s.w;
			}
		}

	}
}


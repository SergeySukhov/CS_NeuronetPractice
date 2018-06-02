using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Neuronet
{
	public class PointNeuron
	{
		public Point point;
		public Neuron neuron;
		List<PointNeuron> linksP = new List<PointNeuron>();

		public PointNeuron(Neuron neu, Point p)
		{
			point = p;
			neuron = neu;			
		}

		public static List<PointNeuron> BuildPoints(NeuroNet neuronet, int width, int height)
		{
			List<PointNeuron> result = new List<PointNeuron>();
						

			int allLayersCount = neuronet.HiddenLayers.Count + 2;

			int maxNeuronsHeight = 1;// neuronet.HiddenLayers[0].neurons.Count;

			foreach (var s in neuronet.HiddenLayers)				
				if (s.neurons != null && maxNeuronsHeight < s.neurons.Count) maxNeuronsHeight = s.neurons.Count;

			int leftborder = 10, topborder = 10;


			for(int i=0; i<neuronet.InputLayer.neurons.Count; i++)
			{
				result.Add(new PointNeuron(neuronet.InputLayer.neurons[i], new Point(leftborder, topborder + i * (height - topborder) / maxNeuronsHeight)));
			}

			if (neuronet.HiddenLayers != null)
				for (int j = 0; j < neuronet.HiddenLayers.Count; j++)
					if (neuronet.HiddenLayers[j] != null && neuronet.HiddenLayers[j].neurons != null)
						for (int i = 0; i < neuronet.HiddenLayers[j].neurons.Count; i++)
						{
							PointNeuron pn = new PointNeuron(neuronet.HiddenLayers[j].neurons[i], new Point(leftborder + (j + 1) * width / allLayersCount, topborder + i * (height - topborder) / maxNeuronsHeight));
							
							result.Add(pn);
						}

			if(neuronet.OutputLayer!= null && neuronet.OutputLayer.neurons!= null)
			for (int i = 0; i < neuronet.OutputLayer.neurons.Count; i++)
			{
				result.Add(new PointNeuron(neuronet.OutputLayer.neurons[i], new Point(leftborder + (allLayersCount-1) * width / allLayersCount, topborder + i * (height - topborder) / maxNeuronsHeight)));
			}

			foreach(var s in result)
			{
				if(s.neuron.outLinks!=null && s.neuron.outLinks.Count > 0)
				foreach (var l in s.neuron.outLinks)
					foreach (var k in result)
						if (l.inNeuron == k.neuron) s.linksP.Add(k);
			
			}

			return result;
		}


		public static Bitmap Draw(NeuroNet nn, int width, int height, out List<PointNeuron> pointNeuronList)
		{
			Bitmap bitmap = new Bitmap(width, height);
			Graphics gr = Graphics.FromImage(bitmap);
			Pen neoPen = new Pen(Color.DarkBlue, 3);
			Pen linkPen = new Pen(Color.Black, 1);

			var pointList = BuildPoints(nn, width, height);
			pointNeuronList = pointList;

			foreach(var s in pointList)
			{
				gr.DrawEllipse(neoPen, new Rectangle(s.point, new Size(10, 10)));
				foreach (var l in s.linksP) gr.DrawLine(linkPen, l.point, s.point);
			}



			return bitmap;
		}

		public static Bitmap Draw(NeuroNet nn, int width, int height)
		{
			Bitmap bitmap = new Bitmap(width, height);
			Graphics gr = Graphics.FromImage(bitmap);
			Pen neoPen = new Pen(Color.DarkBlue, 3);
			Pen linkPen = new Pen(Color.Black, 1);

			var pointList = BuildPoints(nn, width, height);
			

			foreach (var s in pointList)
			{
				gr.DrawEllipse(neoPen, new Rectangle(s.point, new Size(10, 10)));
			}



			return bitmap;
		}




	}
}

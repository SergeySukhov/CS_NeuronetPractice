using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Neuronet;
using Standartnet;
using Kochonennet;

namespace TryDrawNet
{
	public partial class DrawingForm : Form
	{
		public static StandartNet net;
		public static KochonenNet knet;


		double koef = 2.9, param = 0.01;
		Bitmap bitmap;
		Graphics gr;
		Pen neoPen = new Pen(Color.DarkBlue, 3);
		Pen linkPen = new Pen(Color.Red, 1);

		public static int shiftX = 0, shiftY = 0, widthN = 10, heightN = 10;
		public Random randNum = new Random();

		

		List<PointNeuron> listPointNeuron;

		public DrawingForm()
		{
			InitializeComponent();
			bitmap = new Bitmap(pictureBox_NeuroWorking.Width, pictureBox_NeuroWorking.Height);
			gr = Graphics.FromImage(bitmap);


			net = new StandartNet(2, 6, 4, 1);
			knet = new KochonenNet(3, 4);
			//pictureBox_NeuroShedule.Image = DrawNeuronet(pictureBox_NeuroShedule.Width, pictureBox_NeuroShedule.Height);

			pictureBox_NeuroShedule.Image = PointNeuron.Draw(knet, pictureBox_NeuroShedule.Width, pictureBox_NeuroShedule.Height, out listPointNeuron);


			net.ClearImps();
		}

		private void button_Study_Click(object sender, EventArgs e)
		{
			gr.Clear(BackColor);


			int suc = 0;
			double x = 0;// y = 0, z = 0;
			bool wasshift = false;



			for (int j = 0; j < 1000; j++)
			{

				net.ClearImps();

				
				net.InputLayer.neurons[0].imp = (randNum.NextDouble() + 0.1) / 1.3d + 0.1;
				x = net.InputLayer.neurons[0].imp;
				net.InputLayer.neurons[1].imp = 1d;				

				net.RunIteration();

				gr.DrawEllipse(neoPen, (float)(pictureBox_NeuroWorking.Width * (x)), (float)(pictureBox_NeuroWorking.Height * (net.OutputLayer.neurons[0].imp + 0.5) / 2d), 1, 1);
				gr.DrawEllipse(linkPen, (float)(pictureBox_NeuroWorking.Width * (x)), (float)(pictureBox_NeuroWorking.Height * (func2(x) + 0.5) / 2d), 1, 1);

				if (Math.Abs(net.OutputLayer.neurons[0].imp - func2(x)) < param) suc++;

				if (suc > 8500 && !wasshift)
				{
					param /= 1.1d;
					koef /= 2d;
					wasshift = true;
				}

				net.StandartStudy(func2(x), koef);



			}
			textBoxInfo.Text = suc.ToString();
			pictureBox_NeuroWorking.Image = bitmap;
		}

		private void button_test_Click(object sender, EventArgs e)
		{
			net.ClearImps();
			for (int i = 0; i < net.InputLayer.neurons.Count - 1; i++)
			{
				net.InputLayer.neurons[i].imp = 0.4;
			}

			net.InputLayer.neurons[net.InputLayer.neurons.Count - 1].imp = 1d;

			net.RunIteration();

			textBoxInfo.Text = net.OutputLayer.neurons[0].imp.ToString() + Environment.NewLine + func2(0.4);

		}

		private void button_divideKoef_Click(object sender, EventArgs e)
		{
			koef /= 2d;
			textBoxInfo.Text = "koef: " + koef;
		}


		List<Point> neuroPoint = new List<Point>();

		private void button_Koh_Click(object sender, EventArgs e)
		{
			gr.Clear(BackColor);
			for (int i = 0; i < knet.HiddenLayers.Count; i++)
				for (int j = 0; j < knet.HiddenLayers[i].neurons.Count; j++)
			{
				VectorRGB s = new VectorRGB(
					knet.HiddenLayers[i].neurons[j].inLinks[0].w,
					knet.HiddenLayers[i].neurons[j].inLinks[1].w, 
					knet.HiddenLayers[i].neurons[j].inLinks[2].w
					);

				gr.DrawEllipse(new Pen(s.ToColor(), 10), 20*(i+1), 50 * (j + 1), 20, 20);
				neuroPoint.Add(new Point(20*(i+1), 50 * (j + 1) + 10));
			}

			double koefKoh = 1.3, addingKoef = 0.1;

			for (int i = 0; i < vectors.Count; i++)
			{
				knet.InputLayer.neurons[0].imp = vectors[i].r / 255d;
				knet.InputLayer.neurons[1].imp = vectors[i].g / 255d;
				knet.InputLayer.neurons[2].imp = vectors[i].b / 255d;

				knet.RunIteration();

				int winneri = 0, winnerj = 0;

				var b = knet.Study(koefKoh, out winneri, out winnerj, addingKoef);

				if(b) pictureBox_NeuroShedule.Image = PointNeuron.Draw(knet, pictureBox_NeuroShedule.Width, pictureBox_NeuroShedule.Height, out listPointNeuron);

				//gr.DrawRectangle(new Pen(vectors[i].ToColor(), 10), (float)(pictureBox_NeuroWorking.Width * i / vectors.Count), neuroPoint[neunum].Y, 10, 10);

				knet.ClearImps();

			}

			//!
			textBoxInfo.Text = "";
			for (int i = 0; i < knet.HiddenLayers[0].neurons.Count; i++)
			{
				for (int j = 0; j < knet.HiddenLayers[0].neurons[i].inLinks.Count; j++)
					textBoxInfo.Text += "hl" + i + "," + j + ": " + knet.HiddenLayers[0].neurons[i].inLinks[j].w + Environment.NewLine;
				textBoxInfo.Text += Environment.NewLine;
			}

			pictureBox_NeuroWorking.Image = bitmap;
		}

		public List<VectorRGB> vectors = new List<VectorRGB>();

		private void button_addVRGB_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 20; i++)
			{
				vectors.Add(new VectorRGB(255));
			}
			

			for (int i = 0; i < vectors.Count; i++)
				gr.DrawRectangle(new Pen(vectors[i].ToColor(), 10), 5, i * 20, 10, 10);
			
			pictureBox_NeuroWorking.Image = bitmap;
		}

		private void button_multiplyKoef_Click(object sender, EventArgs e)
		{
			koef *= 2d;
			textBoxInfo.Text = "koef: " + koef;
		}		

		private void pictureBox_NeuroShedule_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				foreach(var s in listPointNeuron)
				{
					if (Math.Abs(s.point.X + widthN / 2d - e.X) < widthN && Math.Abs(s.point.Y + widthN / 2d - e.Y) < heightN)
					{
						textBoxInfo.Text += Environment.NewLine + Environment.NewLine + "impuls: " + s.neuron.imp;
						for (int k = 0; k < s.neuron.outLinks.Count; k++)
						{
							textBoxInfo.Text += Environment.NewLine + "w" + k + " : " + s.neuron.outLinks[k].w;
						}
					}
				}

				
			}
			catch (Exception)
			{
				return;
			}

			
		}	
		

		
		double func1(double x, double y, double z)
		{
			return ((x + y + z) / 3d) * ((x + y + z) / 3d);
		}

		double func2(double x)
		{
			//return (x-0.5) * (x-0.5)*4d;
			return Math.Sin(x * 10d) / 2d;
		}


		

	}
}

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Neuronet
{
    public abstract class NeuroNet
    {
		public Layer InputLayer;
		public List<Layer> HiddenLayers;
		public Layer OutputLayer;

		public NeuroNet()
		{
			InputLayer = null;
			HiddenLayers = null;
			OutputLayer = null;
		}



		public virtual void RunIteration()
		{
			for (int i = 0; i < HiddenLayers.Count; i++)
				foreach (var n in HiddenLayers[i].neurons)
				{
					n.sumImp();
				}

			foreach (var s in OutputLayer.neurons)
				s.sumImp();

		}

		public void ClearImps()
		{
			foreach (var s in InputLayer.neurons) s.imp = 0d;

			foreach (var l in HiddenLayers)
				foreach (var s in l.neurons) s.imp = 0d;

			foreach (var s in OutputLayer.neurons) s.imp = 0d;
		}

		private KeyValuePair<int, int> findNeuron(Neuron aimNueron)
		{
			KeyValuePair<int, int> result = new KeyValuePair<int, int>(-1, -1);

			if(InputLayer!=null && InputLayer.neurons != null)
			for(int i = 0; i<InputLayer.neurons.Count; i++)
			{
				if(InputLayer.neurons[i] == aimNueron)
				{
					result = new KeyValuePair<int, int>(0, i);
					return result;
				}

			}

			if(OutputLayer!=null && OutputLayer.neurons != null)
			for (int i = 0; i < OutputLayer.neurons.Count; i++)
			{
				if (OutputLayer.neurons[i] == aimNueron)
				{
					result = new KeyValuePair<int, int>(HiddenLayers.Count + 1, i);
					return result;
				}

			}

			if(HiddenLayers != null)
			for (int i = 0; i < HiddenLayers.Count; i++)
			{
				if(HiddenLayers[i]!=null && HiddenLayers[i].neurons != null)
				for (int j = 0; j < HiddenLayers[i].neurons.Count; j++)
				{
					if (HiddenLayers[i].neurons[j] == aimNueron)
					{
						result = new KeyValuePair<int, int>( i+1, j);
						return result;
					}
				}
			}

			return result;
		}

		private XElement toXml()
		{
			XElement neuronetXml = new XElement("Neuronet");
			
			if(InputLayer!=null && InputLayer.neurons != null)
			for (int i = 0; i < InputLayer.neurons.Count; i++)
			{
				foreach (var s in InputLayer.neurons[i].outLinks)
				{
					XElement linkXml = new XElement("link");

					var directNeuronNum = findNeuron(s.inNeuron);

					XAttribute fromCoord = new XAttribute("fromCoord", "0," + i);
					XAttribute toCoord = new XAttribute("toCoord", directNeuronNum.Key + "," + directNeuronNum.Value);
					XAttribute value = new XAttribute("value", s.w);

					linkXml.Add(fromCoord, toCoord, value);

					neuronetXml.Add(linkXml);

				}
			}

			if (HiddenLayers != null)
			for (int i = 0; i < HiddenLayers.Count; i++)
			{
				if(HiddenLayers[i].neurons!= null)
				for (int j = 0; j < HiddenLayers[i].neurons.Count; j++)
				{
					foreach (var s in HiddenLayers[i].neurons[j].outLinks)
					{
						XElement linkXml = new XElement("link");

						var directNeuronNum = findNeuron(s.inNeuron);

						XAttribute fromCoord = new XAttribute("fromCoord", (i + 1) + "," + j);
						XAttribute toCoord = new XAttribute("toCoord", directNeuronNum.Key + "," + directNeuronNum.Value);
						XAttribute value = new XAttribute("value", s.w);

						linkXml.Add(fromCoord, toCoord, value);

						neuronetXml.Add(linkXml);

					}
				}
			}

			if (OutputLayer != null && OutputLayer.neurons != null)
			for (int i = 0; i < OutputLayer.neurons.Count; i++)
			{
				foreach (var s in OutputLayer.neurons[i].outLinks)
				{
					XElement linkXml = new XElement("link");

					var directNeuronNum = findNeuron(s.inNeuron);

					XAttribute fromCoord = new XAttribute("fromCoord", (HiddenLayers.Count + 1) + "," + i);
					XAttribute toCoord = new XAttribute("toCoord", directNeuronNum.Key + "," + directNeuronNum.Value);
					XAttribute value = new XAttribute("value", s.w);

					linkXml.Add(fromCoord, toCoord, value);

					neuronetXml.Add(linkXml);

				}
			}

			return neuronetXml;
		}

		public string SaveXml() // saving neuronet links and weights, return path
		{
			string localpath = Environment.CurrentDirectory + "\\SavedNeuronets";
			
			if (!Directory.Exists(localpath)) Directory.CreateDirectory(localpath);
						
			string id = Guid.NewGuid().ToString();

			XDocument savingDoc = new XDocument();

			XAttribute netid = new XAttribute("id", id);

			XElement root = this.toXml();//new XElement("Neuronets");
			savingDoc.Add(root);

			//root.Add( this.toXml());

			root.Add(netid);

			string fullPath = Path.Combine(localpath, id + ".xml");
			try
			{
				savingDoc.Save(fullPath);
			}
			catch (Exception)
			{
				fullPath = "error";
				// todo: add logging
			}

			return fullPath;

		}

		public string SaveXml(string neuronetID) // saving neuronet links and weights (with neuronet's id)
		{
			string localpath = Environment.CurrentDirectory + "\\SavedNeuronets";

			if (!Directory.Exists(localpath)) Directory.CreateDirectory(localpath);

			//string id = neuronetID;//Guid.NewGuid().ToString();

			XDocument savingDoc = new XDocument();

			XAttribute netid = new XAttribute("id", neuronetID);

			XElement root = this.toXml();

			root.Add(netid);

			savingDoc.Add(root);

			string fullPath = Path.Combine(localpath, neuronetID + ".xml");
			try
			{				
				savingDoc.Save(fullPath);				
			}
			catch(Exception)
			{
				fullPath = "error";
				// todo: add logging
			}

			return fullPath;
		}
		
		public string LoadXml(string path)// loading xml, return neuronet's id (if it has one)
		{
			string netID = "";
			
			try
			{
				var xdoc = XDocument.Load(path);

				HiddenLayers = new List<Layer>();

				netID = xdoc.Root.Attribute("id").Value;

				foreach (var s in xdoc.Root.Elements("link"))
				{
					KeyValuePair<int, int> fromCoord = new KeyValuePair<int, int>(
						int.Parse(s.Attribute("fromCoord").Value.Split(',')[0]), int.Parse(s.Attribute("fromCoord").Value.Split(',')[1]));

					KeyValuePair<int, int> toCoord = new KeyValuePair<int, int>(
						int.Parse(s.Attribute("toCoord").Value.Split(',')[0]), int.Parse(s.Attribute("toCoord").Value.Split(',')[1]));

					if (toCoord.Key + 1 > HiddenLayers.Count)
					{
						int addingNum = toCoord.Key - HiddenLayers.Count + 1;
						for (int i = 0; i < addingNum; i++)
						{
							HiddenLayers.Add(new Layer());
							HiddenLayers[HiddenLayers.Count-1].neurons = new List<Neuron>();
						}
					}
					if (toCoord.Value + 1 > HiddenLayers[toCoord.Key].neurons.Count)
					{
						int addingNum = toCoord.Value - HiddenLayers[toCoord.Key].neurons.Count + 1;
						for (int i = 0; i < addingNum; i++)
							HiddenLayers[toCoord.Key].neurons.Add(new Neuron());

					}

					if (fromCoord.Key + 1 > HiddenLayers.Count)
					{
						int addingNum = fromCoord.Key - HiddenLayers.Count + 1;
						for (int i = 0; i < addingNum; i++)
						{
							HiddenLayers.Add(new Layer());
							HiddenLayers[HiddenLayers.Count - 1].neurons = new List<Neuron>();
						}
					}

					if (fromCoord.Value + 1 > HiddenLayers[fromCoord.Key].neurons.Count)
					{
						int addingNum = fromCoord.Value - HiddenLayers[fromCoord.Key].neurons.Count + 1;
						for (int i = 0; i < addingNum; i++)
							HiddenLayers[fromCoord.Key].neurons.Add(new Neuron());
					}

					

					Link link = new Link(HiddenLayers[toCoord.Key].neurons[toCoord.Value], HiddenLayers[fromCoord.Key].neurons[fromCoord.Value]);
					link.w = double.Parse(s.Attribute("value").Value, System.Globalization.CultureInfo.InvariantCulture);

				}

				InputLayer = HiddenLayers[0];
				OutputLayer = HiddenLayers[HiddenLayers.Count - 1];

				HiddenLayers.Remove(InputLayer);
				HiddenLayers.Remove(OutputLayer);
			}
			catch(Exception)
			{
				return "error";
			}

			return netID;
		}
	}
}

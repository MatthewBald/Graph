using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graph
{
	public class GraphBuilder
	{
		private Network _network;

		public GraphBuilder(string filePath)
		{
			_network = new Network();
			try
			{
				using (StreamReader sr = new StreamReader(filePath))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] lineArr = line.Split(',');

						Vertex a = new Vertex(lineArr[0].Trim());
						Vertex b = new Vertex(lineArr[1].Trim());
						
						if (_network.GetVertex(a.GetId()) == null) _network.AddVertex(a);
						if (_network.GetVertex(b.GetId()) == null) _network.AddVertex(b);
						if (_network.GetEdge(new Edge(a, b)) == null) _network.AddEdge(a, b);
						
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

			//_network.PrintVertices();
			int cComps = _network.BFS();
			Console.WriteLine(cComps);
		}
	}
}

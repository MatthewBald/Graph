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

						Vertex a = new Vertex(lineArr[0]);
						Vertex b = new Vertex(lineArr[1]);

						Console.WriteLine(".");

						_network.AddVertex(a);
						_network.AddVertex(b);
						_network.AddEdgeUndirected(a, b);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

			_network.DFS();
		}
	}
}

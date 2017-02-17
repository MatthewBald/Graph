using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Graph
{
	class GraphBuilder
	{
		private Network _network;
		private FileStream _fin;

		public GraphBuilder(string filePath)
		{
			_fin = new FileStream(filePath, FileMode.Open);

		}
	}
}

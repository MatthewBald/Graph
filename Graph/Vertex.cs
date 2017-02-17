using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
	class Vertex : IEquatable<Vertex>
	{
		private int _id;
		private List<int> _connections;
		private string _label;
		private Vertex _back;
		
		public Vertex(int id)
		{
			_id = id;
			_connections = new List<int>();
			_label = "UNEXPLORED";
			_back = null;
		}
		
		public int GetId()
		{
			return _id;
		}

		public string GetLabel()
		{
			return _label;
		}

		public void SetLabel(string label)
		{
			_label = label;
		}

		public List<int> GetConnections()
		{
			return _connections;
		}

		public void SetConnections(List<int> connections)
		{
			_connections = connections;
		}

		public List<int> AddConnection(int connection)
		{
			_connections.Add(connection);
			return _connections;
		}

		public bool Equals(Vertex other)
		{
			if (other == null)
				return false;

			return (_id == other._id);
		}

	}
}

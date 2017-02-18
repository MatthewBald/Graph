using System;
using System.Collections.Generic;

namespace Graph
{
	class Vertex : IEquatable<Vertex>
	{
		private string _id;
		private List<string> _connections;
		private string _label;
		private Vertex _back;
		
		public Vertex(string id)
		{
			_id = id;
			_connections = new List<string>();
			_label = "UNEXPLORED";
			_back = null;
		}
		
		public string GetId()
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

		public List<string> GetConnections()
		{
			return _connections;
		}

		public void SetConnections(List<string> connections)
		{
			_connections = connections;
		}

		public List<string> AddConnection(string connection)
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

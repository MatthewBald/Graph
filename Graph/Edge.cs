using System;

namespace Graph
{
	/// <summary>
	/// The Edge class is used to describe a directed path between two vertices in a network.
	/// Contains the two vertices it connects, vertex a is the sender and vertex b is the receiver.
	/// Also contains a label that is used for Breadth and Depth first searches. 
	/// </summary>
	class Edge : IEquatable<Edge>
	{
		private Vertex _vOne;
		private Vertex _vTwo;
		private string _label;

		public Edge()
		{
			_vOne = null;
			_vTwo = null;
			_label = "UNEXPLORED";
		}

		public Edge(Vertex a, Vertex b)
		{
			_vOne = a;
			_vTwo = b;
			_label = "UNEXPLORED";
		}

		public Vertex GetVertexA()
		{
			return _vOne;
		}

		public Vertex GetVertexB()
		{
			return _vTwo;
		}
		
		public string GetLabel()
		{
			return _label;
		}

		public void SetLabel(string label)
		{
			_label = label;
		}

		public Edge Conjugate()
		{
			return new Edge(_vTwo, _vOne);
		}
		
		public bool Equals(Edge other)
		{
			if (other == null)
				return false;

			if (other._vOne == _vOne && other._vTwo == _vTwo)
				return true;

			return false;
		}
	}
}

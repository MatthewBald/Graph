using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
	class Network
	{
		private List<Vertex> _vertices;
		private List<Edge> _edges;

		public Network()
		{
			_vertices = new List<Vertex>();
			_edges = new List<Edge>();
		}

		/// <summary>
		/// Searches the network for an edge with the same vertices as Edge e
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Edge GetEdge(Edge e)
		{
			return _edges.Find(x => x.Equals(e));
		}

		/// <summary>
		/// Searches the network for a vertex with matching id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Vertex GetVertex(int id)
		{
			return _vertices.Find(x => x.Equals(id));
		}

		/// <summary>
		/// Adds Vertex v to the Network's List of vertices
		/// </summary>
		/// <param name="v"></param>
		public void AddVertex(Vertex v)
		{
			_vertices.Add(v);
		}

		/// <summary>
		/// Adds a directed edge from Vertex v to Vertex w
		/// </summary>
		/// <param name="v"></param>
		/// <param name="w"></param>
		public void AddEdge(Vertex v, Vertex w)
		{
			if (v == null || w == null)
				return;
			
			_edges.Add(new Edge(v, w));
		}

		/// <summary>
		/// Adds a bi-directional edge between vertices v and w
		/// </summary>
		/// <param name="v"></param>
		/// <param name="w"></param>
		public void AddEdgeUndirected(Vertex v, Vertex w)
		{
			if (v == null || w == null)
				return;

			AddEdge(v, w);
			AddEdge(w, v);
		}

		/// <summary>
		/// Removes a directed edge from the network
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool RemoveEdge(Edge e)
		{
			return _edges.Remove(e);
		}

		/// <summary>
		/// Removes a bi-directional edge from the network
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool RemoveEdgeUndirected(Edge e)
		{
			return _edges.Remove(e) && _edges.Remove(e.Conjugate());
		}

		public List<Edge> IncidentEdges(Vertex v)
		{
			if (v.GetConnections() == null || v.GetConnections().Count == 0)
				return null;

			List<Edge> incidents = new List<Edge>();

			incidents.AddRange(_edges.FindAll(x => x.GetVertexA().GetId() == v.GetId()));

			return incidents;
		}

		public List<Vertex> EndVertices(Edge e)
		{
			if (e == null || e.GetVertexA() == null || e.GetVertexB() == null)
				return null;

			List<Vertex> endPoints = new List<Vertex>();

			endPoints.Add(e.GetVertexA());
			endPoints.Add(e.GetVertexB());

			return endPoints;
		}

	}
}

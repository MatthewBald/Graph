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

		public List<Vertex> GetVertices()
		{
			return _vertices;
		}

		public void SetVertices(List<Vertex> vertices)
		{
			_vertices = vertices;
		}

		public List<Edge> GetEdges()
		{
			return _edges;
		}

		public void SetEdges(List<Edge> edges)
		{
			_edges = edges;
		}

		public void PrintVertices()
		{
			foreach(Vertex v in _vertices)
			{
				Console.WriteLine(v.GetId() + " with label: " + v.GetLabel());
				Console.WriteLine("Connections: " + string.Join(", ", v.GetConnections()));
				Console.WriteLine("");
			}
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
		public Vertex GetVertex(string id)
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
			_vertices.Find(vert => vert.GetId() == v.GetId()).AddConnection(w.GetId());
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

		/// <summary>
		/// Returns a List containing the Edges starting at Vertex v
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public List<Edge> IncidentEdges(Vertex v)
		{
			List<Edge> incidents = new List<Edge>();

			if (v.GetConnections() == null || v.GetConnections().Count == 0)
				return incidents;

			incidents.AddRange(_edges.FindAll(x => x.GetVertexA().GetId() == v.GetId()));

			return incidents;
		}

		/// <summary>
		/// Returns a List containing the two Vertices at either end of Edge e
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public List<Vertex> EndVertices(Edge e)
		{
			if (e == null || e.GetVertexA() == null || e.GetVertexB() == null)
				return null;

			List<Vertex> endPoints = new List<Vertex>();

			endPoints.Add(e.GetVertexA());
			endPoints.Add(e.GetVertexB());

			return endPoints;
		}
		
		/// <summary>
		/// Returns whether Edge e is Directed or not
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool IsDirected(Edge e)
		{
			if (e == null)
				return false;

			string other = e.GetVertexA().GetConnections().Find(id => id == e.GetVertexB().GetId());

			Edge conj = e.Conjugate();
			
			return (other == conj.GetVertexB().GetId());
		}

		/// <summary>
		/// Returns the destination of a directed Edge e
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Vertex Destination(Edge e)
		{
			if (e == null)
				return null;

			return e.GetVertexB();
		}

		/// <summary>
		/// Returns the origin of a directed Edge e
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public Vertex Origin(Edge e)
		{
			if (e == null)
				return null;

			return e.GetVertexA();
		}

		/// <summary>
		/// Returns the opposite Vertex of Vertex v on Edge e
		/// </summary>
		/// <param name="v"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		public Vertex Opposite(Vertex v, Edge e)
		{
			if (v == null || e == null)
				return null;

			if (e.GetVertexA().GetId() == v.GetId())
				return GetVertex(e.GetVertexB().GetId());

			else if (e.GetVertexB().GetId() == v.GetId())
				return GetVertex(e.GetVertexA().GetId());

			return null;
		}


		public bool AreAdjacent(Vertex v, Vertex w)
		{
			if (v == null || w == null)
				return false;

			foreach (string id in v.GetConnections())
				if (id == w.GetId())
					return true;

			foreach (string id in w.GetConnections())
				if (id == v.GetId())
					return true;

			return false;
		}

		public void DFS()
		{
			// Reset the labels of all Vertices/Edges to UNEXPLORED
			_vertices.ForEach(v => v.SetLabel("UNEXPLORED"));
			_edges.ForEach(e => e.SetLabel("UNEXPLORED"));

			foreach (Vertex v in _vertices)
			{
				if (v.GetLabel() == "UNEXPLORED")
				{
					DFS(v);
				}	
			}
		}

		public void DFS(Vertex v)
		{
			v.SetLabel("VISITED");

			Console.WriteLine("");
			Console.WriteLine(v.GetId());
			Console.WriteLine(v.GetLabel());
			
			foreach (Edge e in IncidentEdges(v))
			{
				if (e.GetLabel() == "UNEXPLORED")
				{
					Vertex w = Opposite(v, e);
					if (w != null && w.GetLabel() == "UNEXPLORED")
					{
						e.SetLabel("DISCOVERY");
						DFS(w);
					}
					else e.SetLabel("BACK");
				}
			}
		}

		public int BFS()
		{
			int connectedComponents = 0;

			if (_vertices == null || _edges == null)
				return connectedComponents;

			// Reset the labels of all Vertices/Edges to UNEXPLORED
			_vertices.ForEach(v => v.SetLabel("UNEXPLORED"));
			_edges.ForEach(e => e.SetLabel("UNEXPLORED"));

			foreach (Vertex v in _vertices)
			{
				if (v.GetLabel() == "UNEXPLORED")
				{
					connectedComponents++;
					BFS(v);
				}
			}

			return connectedComponents;
		}

		public void BFS(Vertex v)
		{
			Queue<Vertex> vertexQueue = new Queue<Vertex>();

			vertexQueue.Enqueue(v);
			v.SetLabel("VISITED");

			while (vertexQueue.Count != 0)
			{
				Vertex w = vertexQueue.Dequeue();

				if (w.GetConnections().Count != 0)
				{
					foreach (Edge e in IncidentEdges(w))
					{
						if (e.GetLabel() == "UNEXPLORED")
						{
							Vertex u = Opposite(w, e);

							if (u != null && u.GetLabel() == "UNEXPLORED")
							{
								e.SetLabel("DISCOVERY");
								u.SetLabel("VISITED");
								vertexQueue.Enqueue(u);
							}
							else
							{
								e.SetLabel("CROSS");
							}
						}
					}
				}
			}
		}
	}
}

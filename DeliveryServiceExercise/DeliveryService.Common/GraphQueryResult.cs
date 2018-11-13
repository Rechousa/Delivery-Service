using System.Text;

namespace DeliveryService.Common
{
    public class GraphQueryResult
    {
        public GraphNode[] Nodes { get; set; }
        public GraphRelationship[] Relationships { get; set; }

        public string GetResult()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Relationships.Length; i++)
            {
                sb.Append($"{Nodes[i].Properties.name} (Cost: {Relationships[i].Properties.cost}€, Distance: { Relationships[i].Properties.distance}km) > ");
            }
            sb.Append($"{Nodes[Nodes.Length - 1].Properties.name}");
            return sb.ToString();
        }
    }
}

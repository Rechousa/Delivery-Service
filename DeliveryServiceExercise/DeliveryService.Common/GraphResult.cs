using System.Linq;

namespace DeliveryService.Common
{
    public class GraphResult
    {
        public string Message { get; set; }
        public int TotalCost { get; set; }
        public int TotalDistance { get; set; }

        public GraphResult(GraphQueryResult graphQueryResult)
        {
            Message = graphQueryResult.GetResult();
            TotalCost = graphQueryResult.Relationships.Sum(t => t.Properties.cost);
            TotalDistance = graphQueryResult.Relationships.Sum(t => t.Properties.distance);
        }
    }
}

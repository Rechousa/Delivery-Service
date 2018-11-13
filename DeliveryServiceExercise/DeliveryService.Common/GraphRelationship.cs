namespace DeliveryService.Common
{
    public class GraphRelationship
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int StartNodeId { get; set; }
        public int EndNodeId { get; set; }
        public GraphRelationshipProperties Properties { get; set; }
    }
}

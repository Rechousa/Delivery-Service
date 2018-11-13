using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DeliveryService.Common
{
    public class DeliveryServiceNeo4jClient : IDisposable
    {
        private readonly IDriver _driver;

        public DeliveryServiceNeo4jClient(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public void DeleteAllNodes()
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx => { tx.Run("MATCH (n) DETACH DELETE n"); });
            }
        }

        public void InsertNode(int id, string location)
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx => { tx.Run("CREATE (a:Location { id: $id, name: $location})", new { id, location }); });
            }
        }

        public void ConnectNodes(int locationA, int locationB, int distance, int cost)
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx => { tx.Run("MATCH (a:Location),(b:Location) WHERE a.id = $locationA AND b.id = $locationB CREATE (a)-[r:RELTYPE { distance: $distance, cost: $cost }]->(b) RETURN type(r), r.name;", new { locationA, locationB, distance, cost }); });
            }
        }

        public List<GraphResult> FindShortestRoute(int locationA, int locationB)
        {
            using (var session = _driver.Session())
            {
                return session.WriteTransaction(tx =>
                {
                    var result = tx.Run(@"MATCH p=(a:Location {id: $locationA})-[:RELTYPE*2..4]-(c:Location {id: $locationB})
RETURN p AS shortestPath, 
   reduce(distance=0, r in relationships(p) | distance + r.distance) as n 
   ORDER BY n ASC
   LIMIT 10",
                        new { locationA, locationB });

                    var results = new List<GraphResult>();
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record.Values["shortestPath"]);
                        var graphResult = JsonConvert.DeserializeObject<GraphQueryResult>(nodeProps);

                        results.Add(new GraphResult(graphResult));
                    }
                    return results;
                });
            }
        }

        public List<GraphResult> FindCheapestRoute(int locationA, int locationB)
        {
            using (var session = _driver.Session())
            {
                return session.WriteTransaction(tx =>
                {
                    var result = tx.Run(@"MATCH p=(a:Location {id: $locationA})-[:RELTYPE*2..4]-(c:Location {id: $locationB})
RETURN p AS shortestPath, 
   reduce(cost=0, r in relationships(p) | cost + r.cost) as n 
   ORDER BY n ASC
   LIMIT 10",
                        new { locationA, locationB });

                    var results = new List<GraphResult>();
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record.Values["shortestPath"]);
                        var graphResult = JsonConvert.DeserializeObject<GraphQueryResult>(nodeProps);

                        results.Add(new GraphResult(graphResult));
                    }
                    return results;
                });
            }
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}

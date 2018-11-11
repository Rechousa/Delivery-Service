using DeliveryService.Database;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common
{
    public class RouteVM
    {
        [Required]
        public int LocationA { get; set; }

        [Required]
        public int LocationB { get; set; }

        [Required]
        public int Distance { get; set; }

        [Required]
        public int Cost { get; set; }

        public static RouteVM FromRoute(Route route)
        {
            var routeVM = new RouteVM
            {
                LocationA = route.LocationA,
                LocationB = route.LocationB,
                Distance = route.Distance,
                Cost = route.Cost
            };
            return routeVM;
        }

        public static Route ToRoute(RouteVM route)
        {
            var result = new Route
            {
                LocationA = route.LocationA,
                LocationB = route.LocationB,
                Distance = route.Distance,
                Cost = route.Cost
            };
            return result;
        }

        public static Route UpdateRouteData(Route route, RouteVM routeVM)
        {
            route.LocationA = routeVM.LocationA;
            route.LocationB = routeVM.LocationB;
            route.Distance = routeVM.Distance;
            route.Cost = routeVM.Cost;
            return route;
        }
    }
}

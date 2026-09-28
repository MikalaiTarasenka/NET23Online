using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using AnimalWorld.Web.Hubs;
using AnimalWorld.Web.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace AnimalWorld.Web.Jobs
{
    public class PromotionsCheckJob : IJob
    {
        private IServiceScopeFactory _scopeFactory;

        public PromotionsCheckJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var promotionsRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
                var zooRepository = scope.ServiceProvider.GetRequiredService<IZooRepository>();
                var hub = scope.ServiceProvider.GetRequiredService<IHubContext<PromotionsHub, IPromotionsHub>>();
                var promotionsForDelete = new List<int>();
                var promotions = await promotionsRepository.GetAll();
                foreach (var promotion in promotions)
                {
                    if (promotion.EndDate > DateTime.Now)
                    {
                        var zoo = await zooRepository.GetById(promotion.VenueId);
                        var message = $"В зоопарке {zoo.Name} проходит акция \"{promotion.Name}\".\n\n{promotion.Description}\n\nАкция заканчивается {promotion.EndDate:yyyy-MM-dd}.";
                        await hub.Clients.All.ZooPromotion(message);
                    }
                }
            }
        }
    }
}

using Nop.Core.Domain.Shipping;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;

namespace NH.Plugin.Shipping.StandVdMaan;

public class StandVdMaanComputationMethod : BasePlugin, IShippingRateComputationMethod
{
    public async Task<GetShippingOptionResponse> GetShippingOptionsAsync(GetShippingOptionRequest getShippingOptionRequest)
    {
        var response = new GetShippingOptionResponse();
        
        response.ShippingOptions.Add(new ShippingOption
        {
            Name = "Stand van de maan",
            IsPickupInStore = false,
            TransitDays = 1,
            Rate = getShippingOptionRequest.Items.Count * DateTime.Now.Day,
            ShippingRateComputationMethodSystemName = "NH.Plugin.Shipping.StandVdMaan"
        });
        
        return response;
    }

    public Task<decimal?> GetFixedRateAsync(GetShippingOptionRequest getShippingOptionRequest)
    {
        return Task.FromResult<decimal?>(null);
    }

    public Task<IShipmentTracker> GetShipmentTrackerAsync()
    {
        return Task.FromResult<IShipmentTracker>(new MaanTracker());
    }
}

public class MaanTracker : IShipmentTracker
{
    public Task<string> GetUrlAsync(string trackingNumber, Shipment shipment = null)
    {
        return Task.FromResult("https://www.telegraaf.nl/vrouw/horoscopen");
    }

    public Task<IList<ShipmentStatusEvent>> GetShipmentEventsAsync(string trackingNumber, Shipment shipment = null)
    {
        return Task.FromResult<IList<ShipmentStatusEvent>>(new List<ShipmentStatusEvent>());
    }
}
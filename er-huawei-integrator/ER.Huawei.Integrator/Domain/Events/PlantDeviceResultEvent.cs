using ER.Huawei.Integrator.Cons.Application.Model.Dto;
using ER.Huawei.Integrator.Cons.Domain.Core.Events;
using MongoDB.Bson;

namespace ER.Huawei.Integrator.Domain.Events;

public class PlantDeviceResultEvent : Event
{
    public ObjectId _id { get; set; }
    public string brandName { get; set; } = "brand";
    public string stationCode { get; set; }
    public DateTime repliedDateTime { get; set; }
    public List<DeviceDataResponse<DeviceInverterDataItem>> invertersList { get; set; }
    public List<DeviceDataResponse<DeviceMetterDataItem>> metterList { get; set; }

}

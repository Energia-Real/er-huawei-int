using ER.Huawei.Integrator.Cons.Application.Model;
using MongoDB.Bson;

namespace ER.Huawei.Integrator.Cons.Application.Model.Dto
{
    public class HourProjectResume
    {
        public ObjectId _id { get; set; }
        public string brandName { get; set; } = "brand";
        public string stationCode { get; set; }
        public DateTime repliedDateTime { get; set; }

        public List<HourResumeResponse> HourResume { get; set; }
    }
}

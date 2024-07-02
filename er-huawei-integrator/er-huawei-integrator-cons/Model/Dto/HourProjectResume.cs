﻿using er_huawei_integrator_cons.Application.Model;
using MongoDB.Bson;

namespace er_huawei_integrator_cons.Application.Model.Dto
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

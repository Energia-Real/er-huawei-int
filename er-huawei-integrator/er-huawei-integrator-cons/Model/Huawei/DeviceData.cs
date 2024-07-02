namespace er_huawei_integrator_cons.Application.Model.Huawei
{
    public class DeviceData
    {
        public List<string> data { get; set; }
        public int failCode { get; set; }
        public string message { get; set; }
        public Params @params { get; set; }
        public bool success { get; set; }
    }
}

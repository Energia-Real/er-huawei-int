namespace ER.Huawei.Integrator.Cons.Model.Dto
{
    public class RepliMessagesDto
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string StationCode { get; set; }
        public string TypeMensaje { get; set; }
        public DateTime HoraConsulta { get; set; }
        public DateTime HoraLlegoConsolidador { get; set; }
        public DateTime FechaConsolido { get; set; }
        public int Estatus { get; set; }
    }
}

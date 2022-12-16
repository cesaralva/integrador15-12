namespace WebApiCondominio.Dto
{
    public class ReciboDTO
    {
        public int? IdRecibo { get; set; }
        public int? Iddepartamento { get; set; }
        public string? torre { get; set; }

        public int piso { get; set; }
        public int numero { get; set; }

        public string nombres { get; set; }
        public string apellidos { get; set; }

        public decimal montopago { get; set; }

        public DateTime? fechavencimiento { get; set; }

        public string? Estadopago { get; set; }

    }
}

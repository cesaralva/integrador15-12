namespace WebApiCondominio.Dto
{
    public class ReciboGastosDTO
    {
        public int Id { get; set; }

        public string Concepto { get; set; } = null!;
        public decimal? Monto { get; set; }
        public int IdRecibo { get; set; }
    }

}

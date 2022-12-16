namespace WebApiCondominio.Dto
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }

        public string Torre { get; set; } = null!;

        public int Piso { get; set; }

        public int Numero { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}
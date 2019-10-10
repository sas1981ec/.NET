namespace Domosti.CapaDatos.Modelos
{
    public partial class Visitante
    {
        public string NombreCompleto { get { return string.Format("{0} {1}", Apellidos, Nombres); } }
    }
}

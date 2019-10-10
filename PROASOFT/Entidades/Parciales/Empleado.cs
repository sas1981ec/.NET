namespace PROASOFT.CapaDominio.Entidades
{
    public partial class EMPLEADO
    {
        public string NombreCompleto
        {
            get
            {
                return $"{APELLIDOS} {NOMBRES}";
            }
        }
    }
}

namespace FacturacionApp.Domain.ValueObjects
{
    public class Email
    {
        public string Valor { get;}

        public Email(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("Email requerido");
            if (!valor.Contains('@'))
                throw new ArgumentException("Email inválido");

            Valor = valor;
        }

        //Para pruebas de integracion
        private Email()
        {
            
        }
    }
}

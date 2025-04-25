// Classe Médico
internal class Medico
{
    private Guid Id { get; set; }
    private string Nome { get; set; }
    private string Crm { get; set; }
    private string Especialidade { get; set; }

    private List<Consulta> consultas;

    public Medico(string nome, string crm, string especialidade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Crm = crm;
        Especialidade = especialidade;
        consultas = new List<Consulta>();
    }

    public string GetNome() => Nome;
    public List<Consulta> GetConsultas() => consultas;
    public Guid GetId() => Id;
    public void AdicionarConsulta(Consulta consulta) => consultas.Add(consulta);
    public void RemoverConsulta(Guid consultaId) => consultas.RemoveAll(c => c.GetId() == consultaId);
}

// Classe Paciente
internal class Paciente
{
    private Guid Id { get; }
    private string Nome { get; set; }
    private string Cpf { get; set; }
    private DateOnly DataNascimento { get; set; }

    public Paciente(string nome, string cpf, DateOnly dataNascimento)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
    }

    public string GetNome() => Nome;
    public string GetCpf() => Cpf;
    public DateOnly GetDataNascimento() => DataNascimento;
    public Guid GetId() => Id;
}

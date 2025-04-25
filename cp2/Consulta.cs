// Classe Consulta
internal class Consulta
{
    private Guid Id { get; set; }
    private DateOnly Data { get; set; }
    private TimeOnly Hora { get; set; }
    protected DateTime CriadoUtc { get; set; }

    public Consulta(DateOnly data, TimeOnly hora)
    {
        Id = Guid.NewGuid();
        Data = data;
        Hora = hora;
        CriadoUtc = DateTime.UtcNow;
    }

    public DateOnly GetData() => Data;
    public TimeOnly GetHora() => Hora;
    public Guid GetId() => Id;
}
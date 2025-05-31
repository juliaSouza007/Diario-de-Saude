using System;
public class RegistroCompletoDTO
{
    public DateTimeOffset? Data { get; set; }
    public string? Humor { get; set; }
    public string? Sono { get; set; }
    public string? Alimentacao { get; set; }
    public string? Atividade { get; set; }
    public int Tempo { get; set; }
    public int RegistroId { get; set; }
}
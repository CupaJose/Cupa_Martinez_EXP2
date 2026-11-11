using Juegoteca.Biblioteca.Interfaces;

namespace Juegoteca.Biblioteca.Modelos;

public class Videojuego : IVideojuego
{
    public string Titulo { get; private set; }
    public string Estudio { get; private set; }
    public int HorasJuego { get; private set; }

    public Videojuego(string titulo, string estudio, int horasJuego)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título no puede estar vacío.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(estudio))
            throw new ArgumentException("El estudio no puede estar vacío.", nameof(estudio));
        if (horasJuego <= 0)
            throw new ArgumentException("Las horas de juego deben ser mayores que cero.", nameof(horasJuego));

        Titulo = titulo;
        Estudio = estudio;
        HorasJuego = horasJuego;
    }

    public override string ToString()
    {
        return $"{Titulo} - {Estudio} ({HorasJuego} h)";
    }

    public bool EsIgualA(Videojuego otro)
    {
        if (otro == null) return false;
        return Titulo.Equals(otro.Titulo, StringComparison.OrdinalIgnoreCase)
            && Estudio.Equals(otro.Estudio, StringComparison.OrdinalIgnoreCase);
    }
}

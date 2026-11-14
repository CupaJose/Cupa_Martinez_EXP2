using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;
using Xunit;

namespace Juegoteca.Tests;

public class VideojuegoTests
{
    [Fact]
    public void Constructor_ValoresValidos_AsignaPropiedades()
    {
        Videojuego juego = new Videojuego("Hades", "Supergiant", 25);
        Assert.Equal("Hades", juego.Titulo);
        Assert.Equal("Supergiant", juego.Estudio);
        Assert.Equal(25, juego.HorasJuego);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_HorasInvalidas_LanzaArgumentException(int horas)
    {
        Assert.Throws<ArgumentException>(() => new Videojuego("Hades", "Supergiant", horas));
    }

    [Fact]
    public void Constructor_TituloVacio_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Videojuego(" ", "Supergiant", 25));
    }

    [Fact]
    public void Constructor_EstudioVacio_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Videojuego("Hades", " ", 25));
    }

    [Fact]
    public void ToString_JuegoValido_DevuelveFormatoSolicitado()
    {
        Videojuego juego = new Videojuego("Hades", "Supergiant", 25);
        Assert.Equal("Hades - Supergiant (25 h)", juego.ToString());
    }

    [Fact]
    public void ImplementaIVideojuego_PropiedadesDisponibles()
    {
        IVideojuego juego = new Videojuego("Hades", "Supergiant", 25);
        Assert.Equal("Hades", juego.Titulo);
        Assert.Equal(25, juego.HorasJuego);
    }

    [Fact]
    public void EsIgualA_MismoTituloYEstudio_IgnoraMayusculas()
    {
        Videojuego primero = new Videojuego("Hades", "Supergiant", 25);
        Videojuego segundo = new Videojuego("HADES", "SUPERGIANT", 30);
        Assert.True(primero.EsIgualA(segundo));
    }
}

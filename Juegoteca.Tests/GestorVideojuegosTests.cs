using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;
using Xunit;

namespace Juegoteca.Tests;

public class GestorVideojuegosTests
{
    [Fact]
    public void Constructor_GestorNuevo_CatalogoVacio()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();
        Assert.NotNull(gestor.JuegosDisponibles);
        Assert.Empty(gestor.JuegosDisponibles);
    }

    [Fact]
    public void AgregarJuego_JuegoNuevo_AumentaCatalogo()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();
        gestor.AgregarJuego(new Videojuego("Celeste", "Maddy", 8));
        Assert.Single(gestor.JuegosDisponibles);
    }

    [Fact]
    public void AgregarJuego_Duplicado_NoLoAgrega()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();
        gestor.AgregarJuego(new Videojuego("Celeste", "Maddy", 8));
        gestor.AgregarJuego(new Videojuego("CELESTE", "MADDY", 8));
        Assert.Single(gestor.JuegosDisponibles);
    }

    [Fact]
    public void AgregarJuego_Nulo_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GestorVideojuegos().AgregarJuego(null!));
    }

    [Fact]
    public void EliminarJuego_JuegoExistente_RetornaTrue()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        Assert.True(gestor.EliminarJuego(new Videojuego("Celeste", "Maddy Makes Games", 8)));
        Assert.Equal(7, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void EliminarJuego_JuegoInexistente_RetornaFalse()
    {
        Assert.False(new GestorVideojuegos().EliminarJuego(new Videojuego("X", "Y", 1)));
    }

    [Fact]
    public void EliminarPorIndice_IndiceValido_Elimina()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        Assert.True(gestor.EliminarPorIndice(0));
        Assert.Equal(7, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void EliminarPorIndice_IndiceInvalido_RetornaFalse()
    {
        Assert.False(new GestorVideojuegos().EliminarPorIndice(0));
        Assert.False(new GestorVideojuegos().EliminarPorIndice(-1));
    }

    [Fact]
    public void BuscarPorTitulo_CoincidenciaParcialIgnoraMayusculas()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        Assert.Contains(gestor.BuscarPorTitulo("KNIGHT"), j => j.Titulo == "Hollow Knight");
    }

    [Fact]
    public void BuscarPorTitulo_SinCoincidencias_RetornaListaVacia()
    {
        Assert.Empty(DatosPrueba.GestorPoblado().BuscarPorTitulo("xyz"));
    }

    [Fact]
    public void LimpiarCatalogo_CatalogoPoblado_VaciaLista()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        gestor.LimpiarCatalogo();
        Assert.Empty(gestor.JuegosDisponibles);
    }
}

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
    [Fact]
    public void Constructor_DosGestores_CatalogosIndependientes()
    {
        GestorVideojuegos gestor1 = new GestorVideojuegos();
        GestorVideojuegos gestor2 = new GestorVideojuegos();

        gestor1.AgregarJuego(new Videojuego("Celeste", "Maddy", 8));

        Assert.Single(gestor1.JuegosDisponibles);
        Assert.Empty(gestor2.JuegosDisponibles);
    }

    [Fact]
    public void AgregarJuego_CatalogoPoblado_AumentaAnueve()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();

        gestor.AgregarJuego(new Videojuego("Dead Cells", "Motion Twin", 15));

        Assert.Equal(9, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void AgregarJuego_CatalogoPobladoJuegoDuplicado_ConservaNueve()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        Videojuego juego = new Videojuego("Dead Cells", "Motion Twin", 15);

        gestor.AgregarJuego(juego);
        gestor.AgregarJuego(juego);

        Assert.Equal(9, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void EliminarJuego_CatalogoPobladoJuegoInexistente_ConservaOcho()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();

        bool resultado = gestor.EliminarJuego(
            new Videojuego("Dead Cells", "Motion Twin", 15));

        Assert.False(resultado);
        Assert.Equal(8, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void EliminarPorIndice_CatalogoPobladoIndiceInvalido_ConservaOcho()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();

        Assert.False(gestor.EliminarPorIndice(-1));
        Assert.False(gestor.EliminarPorIndice(8));
        Assert.Equal(8, gestor.JuegosDisponibles.Count);
    }

    [Fact]
    public void BuscarPorTitulo_CatalogoVacio_RetornaListaVacia()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();

        Assert.Empty(gestor.BuscarPorTitulo("Hades"));
    }

    [Fact]
    public void EliminarJuego_CatalogoVacio_RetornaFalse()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();

        Assert.False(gestor.EliminarJuego(
            new Videojuego("Hades", "Supergiant", 25)));
    }
}

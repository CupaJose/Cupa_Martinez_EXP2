using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Tests.Datos;
using Xunit;

namespace Juegoteca.Tests;

public class AlgoritmosTests
{
    [Fact]
    public void QuickSort_ListaDesordenada_OrdenaLaMismaListaPorHoras()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        List<Videojuego> lista = gestor.JuegosDisponibles;
        gestor.QuickSort(lista);
        Assert.Equal(new[] { 5, 8, 10, 25, 40, 60, 100, 120 },
            lista.Select(j => j.HorasJuego));
    }

    [Fact]
    public void QuickSort_ListaVacia_NoLanzaExcepcion()
    {
        new GestorVideojuegos().QuickSort(new List<Videojuego>());
        Assert.Empty(new List<Videojuego>());
    }

    [Fact]
    public void QuickSort_ListaNula_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GestorVideojuegos().QuickSort(null!));
    }

    [Fact]
    public void MergeSort_ListaDesordenada_DevuelveNuevaListaOrdenada()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        List<Videojuego> original = new List<Videojuego>(gestor.JuegosDisponibles);
        List<Videojuego> resultado = gestor.MergeSort(original);

        Assert.NotSame(original, resultado);
        Assert.Equal(new[] { 5, 8, 10, 25, 40, 60, 100, 120 },
            resultado.Select(j => j.HorasJuego));
        Assert.Equal(original, gestor.JuegosDisponibles);
    }

    [Fact]
    public void MergeSort_ListaVacia_DevuelveListaVacia()
    {
        List<Videojuego> resultado = new GestorVideojuegos().MergeSort(new List<Videojuego>());
        Assert.Empty(resultado);
    }

    [Fact]
    public void MergeSort_ListaNula_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GestorVideojuegos().MergeSort(null!));
    }

    [Fact]
    public void QuickSort_HorasRepetidas_MantieneOrdenAscendente()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();
        List<Videojuego> juegos = new List<Videojuego>
        {
            new Videojuego("A", "E", 10),
            new Videojuego("B", "E", 10),
            new Videojuego("C", "E", 2)
        };
        gestor.QuickSort(juegos);
        Assert.Equal(new[] { 2, 10, 10 }, juegos.Select(j => j.HorasJuego));
    }

    [Theory]
    [InlineData("Hades")]
    [InlineData("PORTAL 2")]
    public void BusquedaBinaria_TituloExistente_DevuelveIndice(string titulo)
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        Assert.True(gestor.BusquedaBinaria(titulo) >= 0);
    }

    [Fact]
    public void BusquedaBinaria_TituloInexistente_DevuelveMenosUno()
    {
        Assert.Equal(-1, DatosPrueba.GestorPoblado().BusquedaBinaria("No existe"));
    }

    [Fact]
    public void BusquedaBinaria_CatalogoVacio_DevuelveMenosUno()
    {
        Assert.Equal(-1, new GestorVideojuegos().BusquedaBinaria("Hades"));
    }

    [Fact]
    public void BusquedaBinaria_NoModificaOrdenDelCatalogo()
    {
        GestorVideojuegos gestor = DatosPrueba.GestorPoblado();
        List<string> original = gestor.JuegosDisponibles.Select(j => j.Titulo).ToList();
        gestor.BusquedaBinaria("Hades");
        Assert.Equal(original, gestor.JuegosDisponibles.Select(j => j.Titulo));
    }
}

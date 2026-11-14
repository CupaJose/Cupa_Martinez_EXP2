using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;
using Juegoteca.Tests.Datos;
using Xunit;

namespace Juegoteca.Tests;

public class ServicioVideojuegosTests
{
    [Fact]
    public void Constructor_DependenciasValidas_InicializaPropiedades()
    {
        GestorVideojuegos gestor = new GestorVideojuegos();
        ExportadorPrueba exportador = new ExportadorPrueba();
        ServicioVideojuegos servicio = new ServicioVideojuegos(gestor, exportador);
        Assert.Same(gestor, servicio.Gestor);
        Assert.Same(exportador, servicio.Exportador);
        Assert.Empty(servicio.Usuarios);
    }

    [Fact]
    public void Constructor_GestorNulo_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ServicioVideojuegos(null!, new ExportadorPrueba()));
    }

    [Fact]
    public void Constructor_ExportadorNulo_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ServicioVideojuegos(new GestorVideojuegos(), null!));
    }

    [Fact]
    public void RegistrarUsuario_NombreValido_RegistraYRetornaUsuario()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        Usuario usuario = servicio.RegistrarUsuario("Ana");
        Assert.Equal("Ana", usuario.Nombre);
        Assert.Equal(1, servicio.ContarUsuarios());
    }

    [Fact]
    public void BuscarUsuario_IgnoraMayusculas()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        servicio.RegistrarUsuario("Ana");
        Assert.NotNull(servicio.BuscarUsuario("ANA"));
        Assert.Null(servicio.BuscarUsuario("Luis"));
    }

    [Fact]
    public void EliminarUsuario_ExistenteRetornaTrue_YNoExistenteFalse()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        servicio.RegistrarUsuario("Ana");
        Assert.True(servicio.EliminarUsuario("Ana"));
        Assert.False(servicio.EliminarUsuario("Ana"));
    }

    [Fact]
    public void OrdenarCatalogo_QuickYMerge_OrdenanCatalogo()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(DatosPrueba.GestorPoblado(), new ExportadorPrueba());
        servicio.OrdenarCatalogo("quick");
        Assert.Equal(5, servicio.Gestor.JuegosDisponibles[0].HorasJuego);
        servicio.OrdenarCatalogo("merge");
        Assert.Equal(5, servicio.Gestor.JuegosDisponibles[0].HorasJuego);
    }

    [Fact]
    public void OrdenarCatalogo_AlgoritmoDesconocido_LanzaArgumentException()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        Assert.Throws<ArgumentException>(() => servicio.OrdenarCatalogo("burbuja"));
    }

    [Fact]
    public void OrdenarColeccionYCalcularHoras_OrdenaYCalcula()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        usuario.AgregarJuegoAColeccion("Pendientes", new Videojuego("Hades", "Supergiant", 25));
        usuario.AgregarJuegoAColeccion("Pendientes", new Videojuego("Celeste", "Maddy", 8));
        Assert.Equal(33, servicio.OrdenarColeccionYCalcularHoras(usuario, "Pendientes"));
        Assert.Equal("Celeste", usuario.ObtenerColeccion("Pendientes")[0].Titulo);
    }

    [Fact]
    public void OrdenarColeccionYCalcularHoras_UsuarioNulo_LanzaArgumentNullException()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), new ExportadorPrueba());
        Assert.Throws<ArgumentNullException>(() => servicio.OrdenarColeccionYCalcularHoras(null!, "A"));
    }

    [Fact]
    public void ExportarColecciones_UsuarioValido_DelegaUnaVez()
    {
        ExportadorPrueba exportador = new ExportadorPrueba();
        ServicioVideojuegos servicio = new ServicioVideojuegos(new GestorVideojuegos(), exportador);
        Usuario usuario = new Usuario("Ana");
        servicio.ExportarColecciones(usuario, "salida.txt");
        Assert.Equal(1, exportador.VecesLlamado);
        Assert.Same(usuario, exportador.UsuarioRecibido);
        Assert.Equal("salida.txt", exportador.RutaRecibida);
    }
    [Fact]
    public void ExportarColecciones_UsuarioNulo_LanzaArgumentNullException()
    {
        ServicioVideojuegos servicio = new ServicioVideojuegos(
            new GestorVideojuegos(),
            new ExportadorPrueba());

        Assert.Throws<ArgumentNullException>(
            () => servicio.ExportarColecciones(null!, "salida.txt"));
    }
}
using Juegoteca.Biblioteca.Modelos;
using Xunit;

namespace Juegoteca.Tests;

public class UsuarioTests
{
    [Fact]
    public void Constructor_UsuarioValido_ColeccionesIniciaVacio()
    {
        Usuario usuario = new Usuario("Ana");
        Assert.Equal("Ana", usuario.Nombre);
        Assert.NotNull(usuario.Colecciones);
        Assert.Empty(usuario.Colecciones);
    }

    [Fact]
    public void Constructor_NombreVacio_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Usuario(" "));
    }

    [Fact]
    public void CrearColeccion_NuevaColeccion_AgregaListaVacia()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        Assert.Empty(usuario.ObtenerColeccion("Pendientes"));
    }

    [Fact]
    public void CrearColeccion_Duplicada_LanzaInvalidOperationException()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        Assert.Throws<InvalidOperationException>(() => usuario.CrearColeccion("PENDIENTES"));
    }

    [Fact]
    public void AgregarJuegoAColeccion_JuegoNuevo_AgregaUnaVez()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        Videojuego juego = new Videojuego("Celeste", "Maddy Makes Games", 8);
        usuario.AgregarJuegoAColeccion("Pendientes", juego);
        usuario.AgregarJuegoAColeccion("Pendientes", juego);
        Assert.Single(usuario.ObtenerColeccion("Pendientes"));
    }

    [Fact]
    public void AgregarJuegoAColeccion_ColeccionInexistente_LanzaKeyNotFoundException()
    {
        Usuario usuario = new Usuario("Ana");
        Assert.Throws<KeyNotFoundException>(() =>
            usuario.AgregarJuegoAColeccion("No existe", new Videojuego("Celeste", "Maddy", 8)));
    }

    [Fact]
    public void AgregarJuegoAColeccion_JuegoNulo_LanzaArgumentNullException()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        Assert.Throws<ArgumentNullException>(() => usuario.AgregarJuegoAColeccion("Pendientes", null!));
    }

    [Fact]
    public void EliminarColeccion_ExistenteRetornaTrue_YLuegoFalse()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("Pendientes");
        Assert.True(usuario.EliminarColeccion("Pendientes"));
        Assert.False(usuario.EliminarColeccion("Pendientes"));
    }

    [Fact]
    public void ObtenerColeccion_Inexistente_LanzaKeyNotFoundException()
    {
        Usuario usuario = new Usuario("Ana");
        Assert.Throws<KeyNotFoundException>(() => usuario.ObtenerColeccion("X"));
    }

    [Fact]
    public void ContarJuegos_SumaTodasLasColecciones()
    {
        Usuario usuario = new Usuario("Ana");
        usuario.CrearColeccion("A");
        usuario.CrearColeccion("B");
        usuario.AgregarJuegoAColeccion("A", new Videojuego("Celeste", "Maddy", 8));
        usuario.AgregarJuegoAColeccion("B", new Videojuego("Hades", "Supergiant", 25));
        Assert.Equal(2, usuario.ContarJuegos());
    }

    [Fact]
    public void ContarJuegos_SinColecciones_RetornaCero()
    {
        Assert.Equal(0, new Usuario("Ana").ContarJuegos());
    }

    [Fact]
    public void Colecciones_DosUsuarios_SonIndependientes()
    {
        Usuario ana = new Usuario("Ana");
        Usuario luis = new Usuario("Luis");
        ana.CrearColeccion("Pendientes");
        Assert.Empty(luis.Colecciones);
    }
}

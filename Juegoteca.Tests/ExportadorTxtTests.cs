using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;
using Juegoteca.Tests.Datos;
using Xunit;

namespace Juegoteca.Tests;

public class ExportadorTxtTests
{
    [Fact]
    public void ExportarATxt_RutaValida_CreaArchivo()
    {
        string ruta = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        try
        {
            new ExportadorTxt().ExportarATxt(DatosPrueba.UsuarioConColecciones(), ruta);
            Assert.True(File.Exists(ruta));
        }
        finally
        {
            if (File.Exists(ruta)) File.Delete(ruta);
        }
    }

    [Fact]
    public void ExportarATxt_ContenidoIncluyeJugadorColeccionJuegoYHoras()
    {
        string ruta = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        try
        {
            new ExportadorTxt().ExportarATxt(DatosPrueba.UsuarioConColecciones(), ruta);
            string contenido = File.ReadAllText(ruta);
            Assert.Contains("Ana", contenido);
            Assert.Contains("Pendientes", contenido);
            Assert.Contains("Celeste", contenido);
            Assert.Contains("Total de horas: 8 h", contenido);
        }
        finally
        {
            if (File.Exists(ruta)) File.Delete(ruta);
        }
    }

    [Fact]
    public void ExportarATxt_UsuarioSinColecciones_CreaArchivoConEncabezado()
    {
        string ruta = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        try
        {
            new ExportadorTxt().ExportarATxt(new Usuario("Luis"), ruta);
            string contenido = File.ReadAllText(ruta);
            Assert.Contains("Luis", contenido);
            Assert.DoesNotContain("Colección:", contenido);
        }
        finally
        {
            if (File.Exists(ruta)) File.Delete(ruta);
        }
    }

    [Fact]
    public void ExportarATxt_MismaRuta_SobrescribeContenido()
    {
        string ruta = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");
        try
        {
            ExportadorTxt exportador = new ExportadorTxt();
            exportador.ExportarATxt(DatosPrueba.UsuarioConColecciones(), ruta);
            exportador.ExportarATxt(new Usuario("Luis"), ruta);
            string contenido = File.ReadAllText(ruta);
            Assert.Contains("Luis", contenido);
            Assert.DoesNotContain("Ana", contenido);
        }
        finally
        {
            if (File.Exists(ruta)) File.Delete(ruta);
        }
    }

    [Fact]
    public void ExportarATxt_UsuarioNulo_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new ExportadorTxt().ExportarATxt(null!, "salida.txt"));
    }

    [Fact]
    public void ExportarATxt_RutaVacia_LanzaArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new ExportadorTxt().ExportarATxt(new Usuario("Ana"), " "));
    }
}

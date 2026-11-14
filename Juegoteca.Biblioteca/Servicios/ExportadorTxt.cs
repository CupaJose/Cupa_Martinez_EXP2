using System.Text;
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios;

public class ExportadorTxt : IExportador
{
    public void ExportarATxt(Usuario usuario, string rutaArchivo)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        if (string.IsNullOrWhiteSpace(rutaArchivo))
            throw new ArgumentException("La ruta del archivo no puede estar vacía.", nameof(rutaArchivo));

        string? directorio = Path.GetDirectoryName(Path.GetFullPath(rutaArchivo));
        if (!string.IsNullOrEmpty(directorio))
            Directory.CreateDirectory(directorio);

        using (StreamWriter escritor = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
        {
            escritor.WriteLine("SISTEMA DE GESTIÓN DE VIDEOJUEGOS");
            escritor.WriteLine($"Jugador: {usuario.Nombre}");
            escritor.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
            escritor.WriteLine();

            foreach (var par in usuario.Colecciones)
            {
                escritor.WriteLine($"Colección: {par.Key}");
                int totalHoras = 0;

                foreach (Videojuego juego in par.Value)
                {
                    escritor.WriteLine($"- {juego}");
                    totalHoras += juego.HorasJuego;
                }

                escritor.WriteLine($"Total de horas: {totalHoras} h");
                escritor.WriteLine();
            }
        }
    }
}

namespace Juegoteca.Biblioteca.Interfaces;

public interface IExportador
{
    void Exportar(string nombreUsuario, IEnumerable<IVideojuego> coleccion, string rutaArchivo);
}

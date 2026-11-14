using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Servicios;

public class ServicioVideojuegos
{
    public IGestorVideojuegos Gestor { get; private set; }
    public IExportador Exportador { get; private set; }
    public List<Usuario> Usuarios { get; private set; }

    public ServicioVideojuegos(IGestorVideojuegos gestor, IExportador exportador)
    {
        Gestor = gestor ?? throw new ArgumentNullException(nameof(gestor));
        Exportador = exportador ?? throw new ArgumentNullException(nameof(exportador));
        Usuarios = new List<Usuario>();
    }

    public Usuario RegistrarUsuario(string nombre)
    {
        Usuario usuario = new Usuario(nombre);
        Usuarios.Add(usuario);
        return usuario;
    }

    public Usuario? BuscarUsuario(string nombre)
    {
        foreach (Usuario usuario in Usuarios)
        {
            if (usuario.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                return usuario;
        }
        return null;
    }

    public bool EliminarUsuario(string nombre)
    {
        Usuario? usuario = BuscarUsuario(nombre);
        if (usuario == null) return false;
        Usuarios.Remove(usuario);
        return true;
    }

    public int ContarUsuarios()
    {
        return Usuarios.Count;
    }

    public void OrdenarCatalogo(string algoritmo)
    {
        if (algoritmo == null)
            throw new ArgumentException("Debe indicar un algoritmo.", nameof(algoritmo));

        if (algoritmo.Equals("quick", StringComparison.OrdinalIgnoreCase))
            Gestor.QuickSort(Gestor.JuegosDisponibles);
        else if (algoritmo.Equals("merge", StringComparison.OrdinalIgnoreCase))
        {
            List<Videojuego> ordenada = Gestor.MergeSort(Gestor.JuegosDisponibles);
            Gestor.JuegosDisponibles.Clear();
            Gestor.JuegosDisponibles.AddRange(ordenada);
        }
        else
            throw new ArgumentException("Algoritmo desconocido. Use 'quick' o 'merge'.", nameof(algoritmo));

        Console.WriteLine($"Catálogo ordenado mediante {algoritmo}.");
    }

    public int OrdenarColeccionYCalcularHoras(Usuario usuario, string coleccion)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        List<Videojuego> juegos = usuario.ObtenerColeccion(coleccion);
        Gestor.QuickSort(juegos);

        int total = 0;
        foreach (Videojuego juego in juegos)
            total += juego.HorasJuego;
        return total;
    }

    public void ExportarColecciones(Usuario usuario, string ruta)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        Exportador.ExportarATxt(usuario, ruta);
    }
}

namespace Juegoteca.Biblioteca.Modelos;

public class Usuario
{
    public string Nombre { get; private set; }
    public Dictionary<string, List<Videojuego>> Colecciones { get; private set; }

    public Usuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        Nombre = nombre;
        Colecciones = new Dictionary<string, List<Videojuego>>(StringComparer.OrdinalIgnoreCase);
    }

    public void CrearColeccion(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la colección no puede estar vacío.", nameof(nombre));
        if (Colecciones.ContainsKey(nombre))
            throw new InvalidOperationException("La colección ya existe.");

        Colecciones.Add(nombre, new List<Videojuego>());
    }

    public void AgregarJuegoAColeccion(string nombre, Videojuego juego)
    {
        if (juego == null)
            throw new ArgumentNullException(nameof(juego));
        if (!Colecciones.ContainsKey(nombre))
            throw new KeyNotFoundException($"No existe la colección '{nombre}'.");

        bool existe = false;
        foreach (Videojuego videojuego in Colecciones[nombre])
        {
            if (videojuego.EsIgualA(juego))
            {
                existe = true;
                break;
            }
        }

        if (!existe)
            Colecciones[nombre].Add(juego);
    }

    public bool EliminarColeccion(string nombre)
    {
        return Colecciones.Remove(nombre);
    }

    public List<Videojuego> ObtenerColeccion(string nombre)
    {
        if (!Colecciones.TryGetValue(nombre, out List<Videojuego>? coleccion))
            throw new KeyNotFoundException($"No existe la colección '{nombre}'.");
        return coleccion;
    }

    public int ContarJuegos()
    {
        int total = 0;
        foreach (List<Videojuego> coleccion in Colecciones.Values)
            total += coleccion.Count;
        return total;
    }

    public void MostrarColecciones()
    {
        if (Colecciones.Count == 0)
        {
            Console.WriteLine("El usuario no tiene colecciones.");
            return;
        }

        foreach (var par in Colecciones)
        {
            Console.WriteLine($"Colección: {par.Key}");
            if (par.Value.Count == 0)
                Console.WriteLine("  (Sin videojuegos)");
            foreach (Videojuego juego in par.Value)
                Console.WriteLine($"  - {juego}");
        }
    }
}

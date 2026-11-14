using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores;

public class GestorVideojuegos : IGestorVideojuegos
{
    public List<Videojuego> JuegosDisponibles { get; private set; }

    public GestorVideojuegos()
    {
        JuegosDisponibles = new List<Videojuego>();
    }

    public void AgregarJuego(Videojuego juego)
    {
        if (juego == null)
            throw new ArgumentNullException(nameof(juego));

        foreach (Videojuego existente in JuegosDisponibles)
        {
            if (existente.EsIgualA(juego))
                return;
        }
        JuegosDisponibles.Add(juego);
    }

    public bool EliminarJuego(Videojuego juego)
    {
        if (juego == null) return false;

        for (int i = 0; i < JuegosDisponibles.Count; i++)
        {
            if (JuegosDisponibles[i].EsIgualA(juego))
            {
                JuegosDisponibles.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool EliminarPorIndice(int indice)
    {
        if (indice < 0 || indice >= JuegosDisponibles.Count)
            return false;

        JuegosDisponibles.RemoveAt(indice);
        return true;
    }

    public List<Videojuego> BuscarPorTitulo(string texto)
    {
        List<Videojuego> resultados = new List<Videojuego>();
        if (string.IsNullOrWhiteSpace(texto))
            return resultados;

        foreach (Videojuego juego in JuegosDisponibles)
        {
            if (juego.Titulo.Contains(texto, StringComparison.OrdinalIgnoreCase))
                resultados.Add(juego);
        }
        return resultados;
    }

    public void LimpiarCatalogo()
    {
        JuegosDisponibles.Clear();
    }

    public void QuickSort(List<Videojuego> lista)
    {
        throw new NotImplementedException("El método QuickSort aún no ha sido implementado.");
    }

    public List<Videojuego> MergeSort(List<Videojuego> lista)
    {
        throw new NotImplementedException("El método MergeSort aún no ha sido implementado.");
    }

    public int BusquedaBinaria(string titulo)
    {
        throw new NotImplementedException("El método BusquedaBinaria aún no ha sido implementado.");
    }
}
using Juegoteca.Biblioteca.Interfaces;
using Juegoteca.Biblioteca.Modelos;

namespace Juegoteca.Biblioteca.Gestores;

public partial class GestorVideojuegos : IGestorVideojuegos
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
        if (lista == null)
            throw new ArgumentNullException(nameof(lista));
        QuickSort(lista, 0, lista.Count - 1);
    }

    private void QuickSort(List<Videojuego> lista, int inicio, int fin)
    {
        if (inicio >= fin) return;

        int pivote = lista[fin].HorasJuego;
        int i = inicio - 1;

        for (int j = inicio; j < fin; j++)
        {
            if (lista[j].HorasJuego <= pivote)
            {
                i++;
                Videojuego temporal = lista[i];
                lista[i] = lista[j];
                lista[j] = temporal;
            }
        }

        Videojuego aux = lista[i + 1];
        lista[i + 1] = lista[fin];
        lista[fin] = aux;

        int posicionPivote = i + 1;
        QuickSort(lista, inicio, posicionPivote - 1);
        QuickSort(lista, posicionPivote + 1, fin);
    }

    public List<Videojuego> MergeSort(List<Videojuego> lista)
    {
        if (lista == null)
            throw new ArgumentNullException(nameof(lista));
        if (lista.Count <= 1)
            return new List<Videojuego>(lista);

        int mitad = lista.Count / 2;
        List<Videojuego> izquierda = new List<Videojuego>();
        List<Videojuego> derecha = new List<Videojuego>();

        for (int i = 0; i < mitad; i++)
            izquierda.Add(lista[i]);
        for (int i = mitad; i < lista.Count; i++)
            derecha.Add(lista[i]);

        izquierda = MergeSort(izquierda);
        derecha = MergeSort(derecha);

        return Mezclar(izquierda, derecha);
    }

    private List<Videojuego> Mezclar(List<Videojuego> izquierda, List<Videojuego> derecha)
    {
        List<Videojuego> resultado = new List<Videojuego>();
        int i = 0;
        int j = 0;

        while (i < izquierda.Count && j < derecha.Count)
        {
            if (izquierda[i].HorasJuego <= derecha[j].HorasJuego)
                resultado.Add(izquierda[i++]);
            else
                resultado.Add(derecha[j++]);
        }

        while (i < izquierda.Count)
            resultado.Add(izquierda[i++]);
        while (j < derecha.Count)
            resultado.Add(derecha[j++]);

        return resultado;
    }

    public int BusquedaBinaria(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return -1;

        List<Videojuego> copia = new List<Videojuego>(JuegosDisponibles);
        copia = OrdenarPorTitulo(copia);

        int inicio = 0;
        int fin = copia.Count - 1;

        while (inicio <= fin)
        {
            int medio = inicio + (fin - inicio) / 2;
            int comparacion = string.Compare(copia[medio].Titulo, titulo,
                StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
                return medio;
            if (comparacion < 0)
                inicio = medio + 1;
            else
                fin = medio - 1;
        }

        return -1;
    }

    // La búsqueda binaria trabaja sobre una copia ordenada alfabéticamente.
    // Este método permite recuperar el juego usando el índice devuelto.
    public Videojuego? ObtenerJuegoOrdenadoPorTitulo(int indice)
    {
        List<Videojuego> copia = OrdenarPorTitulo(new List<Videojuego>(JuegosDisponibles));
        if (indice < 0 || indice >= copia.Count)
            return null;
        return copia[indice];
    }

    private List<Videojuego> OrdenarPorTitulo(List<Videojuego> lista)
    {
        // Ordenamiento por inserción para preparar la copia de la búsqueda binaria.
        for (int i = 1; i < lista.Count; i++)
        {
            Videojuego actual = lista[i];
            int j = i - 1;
            while (j >= 0 && string.Compare(lista[j].Titulo, actual.Titulo,
                StringComparison.OrdinalIgnoreCase) > 0)
            {
                lista[j + 1] = lista[j];
                j--;
            }
            lista[j + 1] = actual;
        }
        return lista;
    }
}
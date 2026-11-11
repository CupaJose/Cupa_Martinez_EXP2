namespace Juegoteca.Biblioteca.Interfaces;

public interface IGestorVideojuegos
{
    IReadOnlyList<IVideojuego> JuegosDisponibles { get; }

    void AgregarJuego(IVideojuego juego);
    bool EliminarJuego(IVideojuego juego);
    bool EliminarPorIndice(int indice);
    IReadOnlyList<IVideojuego> BuscarPorTitulo(string texto);
    void LimpiarCatalogo();
    void QuickSort(List<IVideojuego> lista);
    List<IVideojuego> MergeSort(List<IVideojuego> lista);
    int BusquedaBinaria(string titulo);
}

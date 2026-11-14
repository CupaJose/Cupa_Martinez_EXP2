using Juegoteca.Biblioteca.Gestores;
using Juegoteca.Biblioteca.Modelos;
using Juegoteca.Biblioteca.Servicios;

GestorVideojuegos gestor = new GestorVideojuegos();
ExportadorTxt exportador = new ExportadorTxt();
ServicioVideojuegos servicio = new ServicioVideojuegos(gestor, exportador);

// Catálogo inicial solicitado en el examen.
gestor.AgregarJuego(new Videojuego("Celeste", "Maddy Makes Games", 8));
gestor.AgregarJuego(new Videojuego("Hollow Knight", "Team Cherry", 40));
gestor.AgregarJuego(new Videojuego("Minecraft", "Mojang", 100));
gestor.AgregarJuego(new Videojuego("Tetris", "Alexey Pajitnov", 5));
gestor.AgregarJuego(new Videojuego("Portal 2", "Valve", 10));
gestor.AgregarJuego(new Videojuego("Stardew Valley", "ConcernedApe", 60));
gestor.AgregarJuego(new Videojuego("Hades", "Supergiant Games", 25));
gestor.AgregarJuego(new Videojuego("Zelda: BOTW", "Nintendo", 120));

Console.WriteLine("==========================================");
Console.WriteLine(" BIENVENIDO AL SISTEMA DE GESTIÓN DE VIDEOJUEGOS");
Console.WriteLine("==========================================");

string nombreUsuario = LeerTexto("Ingrese su nombre de usuario: ");
Usuario usuarioActual = servicio.RegistrarUsuario(nombreUsuario);

string nombreInicial = LeerTexto("Nombre de su primera colección: ");
usuarioActual.CrearColeccion(nombreInicial);
string coleccionActual = nombreInicial;

bool salir = false;
while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("--- MENÚ PRINCIPAL ---");
    Console.WriteLine($"Colección actual: '{coleccionActual}' ({usuarioActual.ObtenerColeccion(coleccionActual).Count} juegos)");
    Console.WriteLine("1. Mostrar juegos disponibles");
    Console.WriteLine("2. Ordenar colección (QuickSort o MergeSort)");
    Console.WriteLine("3. Búsqueda binaria en catálogo");
    Console.WriteLine("4. Crear nueva colección");
    Console.WriteLine("5. Cambiar de colección actual");
    Console.WriteLine("6. Ver mis colecciones");
    Console.WriteLine("7. Exportar mis colecciones a .txt");
    Console.WriteLine("8. Salir");

    int opcion = LeerEntero("Seleccione una opción: ");

    try
    {
        switch (opcion)
        {
            case 1:
                MostrarCatalogo(gestor);
                break;

            case 2:
                Console.WriteLine("1. QuickSort");
                Console.WriteLine("2. MergeSort");
                int algoritmo = LeerEntero("Seleccione el algoritmo: ");
                List<Videojuego> juegos = usuarioActual.ObtenerColeccion(coleccionActual);

                if (algoritmo == 1)
                {
                    int total = servicio.OrdenarColeccionYCalcularHoras(usuarioActual, coleccionActual);
                    Console.WriteLine($"Colección ordenada con QuickSort. Total: {total} horas.");
                }
                else if (algoritmo == 2)
                {
                    List<Videojuego> ordenada = gestor.MergeSort(juegos);
                    juegos.Clear();
                    juegos.AddRange(ordenada);
                    int total = 0;
                    foreach (Videojuego juego in juegos)
                        total += juego.HorasJuego;
                    Console.WriteLine($"Colección ordenada con MergeSort. Total: {total} horas.");
                }
                else
                    Console.WriteLine("Opción de algoritmo inválida.");
                MostrarColeccion(usuarioActual, coleccionActual);
                break;

            case 3:
                string titulo = LeerTexto("Escriba el título exacto del videojuego: ");
                int indice = gestor.BusquedaBinaria(titulo);

                if (indice == -1)
                {
                    Console.WriteLine("No se encontraron coincidencias exactas.");
                }
                else
                {
                    Videojuego? encontrado = gestor.ObtenerJuegoOrdenadoPorTitulo(indice);
                    Console.WriteLine($"Videojuego encontrado: {encontrado}");
                    Console.WriteLine("1. Agregar a la colección actual");
                    Console.WriteLine("0. Cancelar");
                    int seleccion = LeerEntero("Seleccione una opción: ");

                    if (seleccion == 1 && encontrado != null)
                    {
                        usuarioActual.AgregarJuegoAColeccion(coleccionActual, encontrado);
                        Console.WriteLine("Videojuego agregado (si no estaba previamente).");
                    }
                    else if (seleccion == 0)
                        Console.WriteLine("Operación cancelada.");
                    else
                        Console.WriteLine("Opción inválida; no se realizaron cambios.");
                }
                break;

            case 4:
                string nuevaColeccion = LeerTexto("Nombre de la nueva colección: ");
                usuarioActual.CrearColeccion(nuevaColeccion);
                Console.WriteLine($"Colección '{nuevaColeccion}' creada.");
                break;

            case 5:
                if (usuarioActual.Colecciones.Count == 0)
                {
                    Console.WriteLine("No hay colecciones registradas.");
                    break;
                }

                Console.WriteLine("Colecciones disponibles:");
                foreach (string nombre in usuarioActual.Colecciones.Keys)
                    Console.WriteLine($"- {nombre}");

                string seleccionColeccion = LeerTexto("Nombre de la colección que desea utilizar: ");
                if (usuarioActual.Colecciones.ContainsKey(seleccionColeccion))
                {
                    coleccionActual = seleccionColeccion;
                    Console.WriteLine($"Colección actual: {coleccionActual}");
                }
                else
                    Console.WriteLine("No existe una colección con ese nombre.");
                break;

            case 6:
                usuarioActual.MostrarColecciones();
                break;

            case 7:
                string ruta = LeerTexto("Ruta del archivo (Enter para usar colecciones.txt): ", permitirVacio: true);
                if (string.IsNullOrWhiteSpace(ruta))
                    ruta = Path.Combine(Environment.CurrentDirectory, "colecciones.txt");

                servicio.ExportarColecciones(usuarioActual, ruta);
                Console.WriteLine($"Colecciones exportadas correctamente en: {Path.GetFullPath(ruta)}");
                break;

            case 8:
                salir = true;
                Console.WriteLine("Gracias por utilizar Juegoteca.");
                break;

            default:
                Console.WriteLine("Seleccione una opción del 1 al 8.");
                break;
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Operación no válida: {ex.Message}");
    }
    catch (KeyNotFoundException ex)
    {
        Console.WriteLine($"No se encontró el elemento: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Dato inválido: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Error al trabajar con el archivo: {ex.Message}");
    }
}

static string LeerTexto(string mensaje, bool permitirVacio = false)
{
    while (true)
    {
        Console.Write(mensaje);
        string? entrada = Console.ReadLine();

        if (permitirVacio && string.IsNullOrWhiteSpace(entrada))
            return "";

        if (!string.IsNullOrWhiteSpace(entrada))
            return entrada.Trim();

        Console.WriteLine("El texto no puede estar vacío.");
    }
}

static int LeerEntero(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int numero))
            return numero;

        Console.WriteLine("Ingrese un número entero válido.");
    }
}

static void MostrarCatalogo(GestorVideojuegos gestor)
{
    Console.WriteLine("--- VIDEOJUEGOS DISPONIBLES ---");
    if (gestor.JuegosDisponibles.Count == 0)
    {
        Console.WriteLine("El catálogo está vacío.");
        return;
    }

    for (int i = 0; i < gestor.JuegosDisponibles.Count; i++)
        Console.WriteLine($"{i + 1}. {gestor.JuegosDisponibles[i]}");
}

static void MostrarColeccion(Usuario usuario, string nombre)
{
    Console.WriteLine($"--- {nombre} ---");
    List<Videojuego> juegos = usuario.ObtenerColeccion(nombre);
    if (juegos.Count == 0)
        Console.WriteLine("(Sin videojuegos)");

    foreach (Videojuego juego in juegos)
        Console.WriteLine(juego);
}

# Notas de desarrollo

## feature/algoritmos

- [x] Implementar QuickSort en `GestorVideojuegos`.
- [x] Implementar MergeSort en `GestorVideojuegos`.
- [x] Implementar BusquedaBinaria en `GestorVideojuegos`.
- [x] Eliminar `AlgoritmosVideojuegos.cs`.
- [ ] Ejecutar `dotnet build`.
- [ ] Ejecutar `dotnet test`.

### Pendiente de integración

La compilación está bloqueada por el error CS0246 en `IExportador.cs`, 
debido a que hace referencia a la clase `Usuario`, perteneciente a 
`feature/usuario`.

La rama `feature/algoritmos` depende de `feature/gestor`, que a su vez 
depende de `feature/videojuego`. Ninguna de estas ramas incorpora 
`feature/usuario`.

Queda pendiente resolver esta dependencia durante la integración 
del proyecto y ejecutar las pruebas correspondientes.
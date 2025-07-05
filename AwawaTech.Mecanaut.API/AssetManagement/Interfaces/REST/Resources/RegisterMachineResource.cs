using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Interfaces.REST.Resources;

public record RegisterMachineResource(
    [Required] string SerialNumber,
    [Required] string Name,
    [Required] string Manufacturer,
    [Required] long PlantId,
    [Required] string Model,
    [Required] string Type,
    [Required] double PowerConsumption,
        
    // Nueva propiedad: Lista de métricas
    [Required] List<MetricResource> Metrics // Lista de métricas asociadas a la máquina
);

// Recurso para cada métrica
public record MetricResource(
    [Required] long MetricId,      // Identificador de la métrica
    [Required] double Value,      // Valor de la métrica
    DateTime? MeasuredAt          // Fecha de medición (opcional)
);
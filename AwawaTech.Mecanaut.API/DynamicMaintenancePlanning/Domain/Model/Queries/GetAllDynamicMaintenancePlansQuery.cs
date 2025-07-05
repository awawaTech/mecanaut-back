namespace AwawaTech.Mecanaut.API.DynamicMaintenancePlanning.Domain.Model.Queries
{
    public class GetAllDynamicMaintenancePlansQuery
    {
        public string TenantId { get; set; }
        public string PlantLineId { get; set; }
    }
} 
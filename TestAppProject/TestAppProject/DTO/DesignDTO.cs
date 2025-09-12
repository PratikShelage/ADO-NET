namespace TestAppProject.DTO
{
    public class DesignDTO
    {
        public int? page { get; set; }

        public int? pageSize { get; set; }

        public int? totalPages { get; set; }

        public string? sortByName { get; set; }

        public string? sortByType { get; set; }

        public string? searchByName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Boolean? isPresent { get; set; }
        public Boolean? isAbsent { get; set; }

    }
}

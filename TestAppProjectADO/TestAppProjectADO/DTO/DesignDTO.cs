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

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public Boolean? isPresent { get; set; } = true;
        public Boolean? isAbsent { get; set; } = true;

    }
}

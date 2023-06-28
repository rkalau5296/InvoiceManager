namespace CsvWpf.Domain.Model
{
    public class Employee
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surename { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public Employee() { }
    }
}

namespace Anbaedari_Exam.Models
{
    public class ProductfeaturesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductGroupId { get; set; }
        public decimal Price { get; set; }
        public DateTime Expirationdate { get; set; }
    }
}

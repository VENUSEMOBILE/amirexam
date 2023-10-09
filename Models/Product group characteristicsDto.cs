namespace Anbaedari_Exam.Models
{
    public class Product_group_characteristicsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  
        public int? ParentGroupId { get; set; }
        public string GroupCode { get; set; } = string.Empty;

    }
}

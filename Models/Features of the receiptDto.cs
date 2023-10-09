namespace Anbaedari_Exam.Models
{
    public class Features_of_the_receiptDto
    {
        public int Id { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}

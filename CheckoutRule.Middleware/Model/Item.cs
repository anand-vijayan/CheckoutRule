namespace CheckoutRule.Middleware.Model
{
    public class Item
    {
        public char ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsPromoApplied { get; set; }
        public decimal ReducedPrice { get; set; }
        public string Calculation { get; set; }
    }
}

namespace BookShoppingCartMvc.Models
{
    public class Order
    {
        public int Id { get; set; } 

        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }


        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }


         [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public string? MobilNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        public bool IsPaid {  get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }


    }
}

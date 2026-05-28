namespace BookShoppingCartMvc.ViewModel
{
    public class CheckoutViewModel
    {

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

       // public bool IsPaid {  get; set; }

    }
}

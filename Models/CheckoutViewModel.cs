using System.ComponentModel.DataAnnotations;

namespace EnaStore.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = "Australia";

    public string ShippingMethod { get; set; } = "standard";
    public string? CouponCode { get; set; }

    [Required(ErrorMessage = "Name on card is required")]
    [Display(Name = "Name on Card")]
    public string CardName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Card number is required")]
    [Display(Name = "Card Number")]
    public string CardNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Expiry is required")]
    [Display(Name = "Expiry (MM/YY)")]
    public string CardExpiry { get; set; } = string.Empty;

    [Required(ErrorMessage = "CVC is required")]
    [Display(Name = "CVC")]
    public string CardCvc { get; set; } = string.Empty;

    // Populated from cart
    public List<CartItem> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}

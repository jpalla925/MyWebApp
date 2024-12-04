using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace MyWebApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Value { get; set; }
    }
}
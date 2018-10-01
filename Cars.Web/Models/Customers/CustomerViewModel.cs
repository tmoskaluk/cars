using Cars.Customers.Crud.Entities;
using Cars.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cars.Web.Models.Customers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        public CustomerType Type { get; set; }

        public string IdentityNo { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
    }

    public static class CustomerViewModelExtensions
    {
        public static CustomerViewModel ToViewModel(this Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Type = customer.Type,
                IdentityNo = customer.IdentityNo,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                Street = customer.Street,
                Number = customer.Number,
                Phone = customer.Phone
            };
        }
    }
}

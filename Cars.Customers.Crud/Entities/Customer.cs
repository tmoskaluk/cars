using Cars.Core.Base.Entities;
using Cars.SharedKernel.Enums;
using System;

namespace Cars.Customers.Crud.Entities
{
    public class Customer : AggregateRoot<int>
    {
        private Customer()
        {
        }

        public Customer(CustomerType type, string identityNo, string firstName, string lastName, string city, string street, string number, string phone)
        {
            Validate(firstName, lastName, number, phone);

            Type = type;
            IdentityNo = identityNo;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Number = number;
            Phone = phone;
        }

        public CustomerType Type { get; private set; }

        public string IdentityNo { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string City { get; private set; }

        public string Street { get; private set; }

        public string Number { get; private set; }

        public string Phone { get; private set; }

        public void Update(CustomerType type, string identityNo, string firstName, string lastName, string city, string street, string number, string phone)
        {
            Validate(firstName, lastName, number, phone);

            Type = type;
            IdentityNo = identityNo;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Number = number;
            Phone = phone;
        }

        private static void Validate(string firstName, string lastName, string number, string phone)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException(nameof(lastName));
            if (number?.Length > 10) throw new ArgumentException(nameof(number));
            if (phone?.Length > 20) throw new ArgumentException(nameof(phone));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCSharpSeleniumAutomation.model
{
    public sealed class User
    {
        private string Email { get; }
        private string Firstname { get; }
        private string Lastname { get; }
        private string Address { get; }
        private string City { get; }
        private string State { get; }
        private string Country { get; }
        private string Phone { get; }

        public User(string email, string firstname, string lastname, string address, string city, string state, string country, string phone)
        {
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            City = city;
            State = state;
            Country = country;
            Phone = phone;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   Email == user.Email &&
                   Firstname == user.Firstname &&
                   Lastname == user.Lastname &&
                   Address == user.Address &&
                   City == user.City &&
                   State == user.State &&
                   Country == user.Country &&
                   Phone == user.Phone;
        }

        public override int GetHashCode()
        {
            var hashCode = 1063186798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Firstname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lastname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(State);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            return hashCode;
        }

        public override string ToString() => "User{" +
                "email='" + Email + '\'' +
                ", firstname='" + Firstname + '\'' +
                ", lastname='" + Lastname + '\'' +
                ", address='" + Address + '\'' +
                ", city='" + City + '\'' +
                ", state='" + State + '\'' +
                ", country='" + Country + '\'' +
                ", phone='" + Phone + '\'' +
                '}';
    }
}

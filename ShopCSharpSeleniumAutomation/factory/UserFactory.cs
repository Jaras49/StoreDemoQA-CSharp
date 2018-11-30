using ShopCSharpSeleniumAutomation.model;
using System;
using System.Linq;

namespace ShopCSharpSeleniumAutomation.factory
{
    public sealed class UserFactory
    {
        private static readonly Random rand = new Random();
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string numbers = "0123456789";
        private static readonly int lenght = 10;

        private UserFactory() { }

        public static User CreateRandomUser()
        {
            return new User
                ($"{ GenerateRandomString() }@test.com",
            $"firstname{ GenerateRandomString() }",
            $"lastname{ GenerateRandomString() }",
            $"address { GenerateRandomString() }",
            $"city{ GenerateRandomString() }",
            $"state{ GenerateRandomString() }",
            "Poland",
            GenerateRandomNumber());
        }

        private static string GenerateRandomString() => new string(Enumerable.Repeat(chars + numbers, lenght)
                .Select(s => s[rand.Next(s.Length)]).ToArray());

        private static string GenerateRandomNumber() => new string(Enumerable.Repeat(numbers, lenght)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
    }
}

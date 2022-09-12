using System;
using System.Text.RegularExpressions;

namespace Marajoara.Cinema.Management.Domain.UserAccountModule
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public AccessLevel Level { set; get; }
        public byte[] Photo { get; set; }



        /// <summary>
        /// Validates the current UserAccount
        /// </summary>        
        /// <returns></returns>
        public bool Validate()
        {
            //Mail validation
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(Mail).Success)
                throw new Exception($"Invalid mail!");
            if (string.IsNullOrWhiteSpace(Name))
                throw new Exception($"User Account name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(Password))
                throw new Exception($"User Account password cannot be null or empty.");

            return true;
        }

    }
}

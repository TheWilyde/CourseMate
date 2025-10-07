using CourseMate.Models.Enums;
using DnsClient;
using System.Net.Mail;

namespace CourseMate.Models.Helping_Classes
{
    public static class EmailValidation
    {
        public static bool IsEmailFormatValid(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> IsDomainValidAsync(string email)
        {
            try
            {
                var domain = email.Split('@')[1];
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domain, QueryType.MX);
                return result.Answers.MxRecords().Any();
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> IsEmailValidAsync(string email)
        {
            return IsEmailFormatValid(email) && await IsDomainValidAsync(email);
        }

        public static enumRole IdentifyEnumRole(string email)
        {
            if(string.IsNullOrEmpty(email) || !IsEmailFormatValid(email))
            {
                return enumRole.Guest;
            }

            var domainPart = email.Split('@')[1].ToLower();

            return domainPart switch
            {
                "student.coursemate.com" => enumRole.Student,
                "instructor.coursemate.com" => enumRole.Instructor,
                "coursemate.com" => enumRole.Admin,
                _ => enumRole.Guest
            };

        }

    }
}

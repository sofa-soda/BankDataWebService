using BankDataWebService.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Net.Mime.MediaTypeNames;

namespace BankDataWebService.Data

{
    public class UserGenerator
    {
        private static Random random = new Random();

        public static User GetUserProfile()
        {
            string firstName = GetFirstName();
            string userName = GetUserName(firstName);

            User user = new User
            {
                UserName = userName,
                FirstName = firstName,
                LastName = GetLastName(),
                Email = GetEmail(userName),
                Password = GetPassword(),
                PhoneNo = GetPhoneNo(),
                ProfilePicture = GetImage(),
                StreetAddress = GetStreetAddress(),
                Suburb = GetSuburb(),
                State = GetState(),
                PostalCode = GetPostalCode(),
                Country = "Australia"
            };

            return user;
        }

        private static string GetFirstName()
        {
            List<string> firstNames = new List<string> {"Liam","Emma","Noah","Olivia","Oliver","Ava","Elijah","James","Isabella",
                "William","Mia","Benjamin","Charlotte","Lucas","Amelia","Henry","Evelyn","Alexander","Abigail",
                "Mason","Harper","Michael","Emily","Ethan","Elizabeth","Daniel","Avery","Jacob","Sofia",
                "Logan","Ella","Jackson","Scarlett","Levi","Grace","Sebastian","Chloe","Mateo","Victoria",
                "Jack","Riley","Owen","Aria","Theodore","Lily","Aiden","Layla","Samuel","Nora"};
            return firstNames[random.Next(firstNames.Count)];
        }

        private static string GetLastName()
        {
            List<string> lastNames = new List<string> {"Smith","Johnson","Williams","Brown","Jones","Garcia","Miller","Davis","Rodriguez","Martinez",
                "Hernandez","Lopez","Gonzalez","Wilson","Anderson","Thomas","Taylor","Moore","Jackson","Martin",
                "Lee","Perez","Thompson","White","Harris","Sanchez","Clark","Ramirez","Lewis","Robinson",
                "Walker","Young","Allen","King","Wright","Scott","Torres","Nguyen","Hill","Flores",
                "Green","Adams","Nelson","Baker","Hall","Rivera","Campbell","Mitchell","Carter","Roberts"};
            return lastNames[random.Next(lastNames.Count)];
        }

        private static string GetUserName(string firstName)
        {
            string randomNumber = random.Next(1000, 10000).ToString();
            return firstName + randomNumber;
        }

        private static string GetEmail(string username)
        {
            return $"{username}@gmail.com";
        }

        private static string GetPassword()
        {
            return random.Next(10000000, 100000000).ToString();
        }

        private static string GetPhoneNo()
        {
            return random.Next(0400000000, 0499999999).ToString();
        }

        private static string GetStreetAddress()
        {
            List<string> streetAddresses = new List<string> { "1123 Oceanic Drive", "5074 Willowbrook Crescent", "1347 Rosewood Avenue",
              "6124 Bellflower Way", "4501 Riverbend Blvd", "9210 Palms Reach", "3792 Bluegum Rd", "2991 Lakeside Dr",
              "1876 Sunflower Lane", "5553 Cedar Grove Rd", "2384 Silver Birch Rd", "8576 Marlowe St"};
            return streetAddresses[random.Next(streetAddresses.Count)];
        }

        private static string GetSuburb()
        {
            List<string> suburbs = new List<string>
            {
                "Mackay", "Ballarat", "Townsville", "Rockhampton", "Launceston",
                "Bendigo", "Albury", "Bundaberg", "Wagga Wagga", "Toowoomba",
                "Coffs Harbour", "Ballina", "Mudgee", "Port Macquarie", "Geraldton",
                "Hervey Bay", "Lismore", "Mount Gambier", "Launceston", "Bunbury"
            };
            return suburbs[random.Next(suburbs.Count)];
        }

        private static string GetState()
        {
            List<string> states = new List<string>
            {
                "NSW", "VIC", "QLD", "SA", "WA", "ACT", "TAS", "NT"
            };
            return states[random.Next(states.Count)];
        }

        private static int GetPostalCode()
        {
            return random.Next(1000, 10000);
        }

        private static byte[] GetImage()
        {
            List<string> imagePaths = new List<string> { "Images/01.jpg", "Images/02.jpg", "Images/03.jpg", "Images/04.jpg",
            "Images/05.jpg", "Images/06.jpg", "Images/07.jpg", "Images/08.jpg"};
            string imagePath = imagePaths[random.Next(imagePaths.Count)];
            return File.ReadAllBytes(imagePath);
        }
    }
}

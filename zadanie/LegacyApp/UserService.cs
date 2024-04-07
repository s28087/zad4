using System;

namespace LegacyApp
{

    public interface ICreditLimitService
    {
        int GetCreditLimit(string lName, DateTime bDate);
    }

    public interface IClientRepository
    {
        Client GetById(int IdClient);
    }
    
    public class UserService
    {

        private IClientRepository _clientRepository;
        
        //dodane
        private ICreditLimitService _creditLimitService;

        //wstrzykiwanie zaleznosci poprzez konstruktor
        public UserService(IClientRepository clientRepository, ICreditLimitService creditLimitService)
        {
            _clientRepository = clientRepository;
            _creditLimitService = creditLimitService;
        }
        
        
        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }
        
        //walidacja uzytkownika w jednej metodzie
        private bool CheckUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (!email.Contains("@")|| !email.Contains("."))
                return false;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) 
                age--;

            return age >= 21;
        }
        
        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }


        public void SetCreditLimit(User user)
        {
            if (user.Client is Client client)
            {
                if (client.Type == "VeryImportantClient")
                    user.HasCreditLimit = false;
                else if (client.Type == "ImportantClient")
                    user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
                else
                {
                    user.HasCreditLimit = true;
                    user.CreditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                }
                    
            }
        }

        public bool CheckCreditLimit(User user)
        {
            return !(user.HasCreditLimit && user.CreditLimit < 500);
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            /*if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }*/
            
            if (!CheckUser(firstName, lastName, email, dateOfBirth))
                return false;

            //inversion control
            //var clientRepository = new ClientRepository();
            //var client = clientRepository.GetById(clientId);
            Client client = _clientRepository.GetById(clientId);

            /*var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };*/
            
            User user = CreateUser(firstName, lastName, email, dateOfBirth, client);
            
            SetCreditLimit(user);

            /*if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                //using (var userCreditService = new UserCreditService())
                //{
                    //int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                //}
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }*/
            
            if (!CheckCreditLimit(user))
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
        
        
        
    }
}

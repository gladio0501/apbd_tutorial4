using System;

namespace LegacyApp;

public class UserService : IUserService
{

    

    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (!Validate(firstName, lastName, email, dateOfBirth))
        {
            return false;
        }

        var clientRepository = new ClientRepository();
        var client = clientRepository.GetById(clientId);

        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        user.CreditLimit = Calculate(client, user);

        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }

        UserDataAccess.AddUser(user);
        return true;
    }

    public bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email))
        {
            return false;
        }
        
        if (!email.Contains("@"))
        {
            return false;
        }

        if (dateOfBirth == default)
        {
            return false;
        }

        return true;
    }

    public int Calculate(Client client, User user)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (client.Type == "VeryImportantClient")
        {
            user.HasCreditLimit = true;
            return 40000;
        }

        if (user.DateOfBirth < DateTime.Now.AddYears(-18))
        {
            user.HasCreditLimit = true;
            return 500;
        }

        if (user.DateOfBirth < DateTime.Now.AddYears(-21))
        {
            user.HasCreditLimit = true;
            return 1000;
        }

        user.HasCreditLimit = true;
        return 2000;
    }
}
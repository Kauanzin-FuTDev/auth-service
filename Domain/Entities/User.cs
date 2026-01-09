using Domain.Entities.Commun;
using Domain.Exceptions;

namespace Domain.Entities;

public class User : BaseEntity
{
    #region Propreties

    public string Name { get; private set; }
    public int PhoneNumber { get; private set; }
    public string Address { get; private set; }
    public string CPF { get; private set; }
    public bool Gender  { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime Age { get; private set; } 
    
    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    
  
    

    #endregion

    #region Constructor

    public User()
    {
        
    }
    
    public User(string name, string email, string password, DateTime age, string cpf)
    {
        Name = name;
        Email = email;
        Password = password;
        Age = age;
        CPF = cpf;
    }

    #endregion

    #region Methods
    public static User CreateUser(string name, string email, string password, List<Role> roles, DateTime age, int phoneNumber, string address,
        string cpf, bool gender)
    {
        
        Validation(name, email, password, age,cpf);

        var user = new User(name, email, password, age, cpf)
        {
            Name = name,
            Email = email,
            Password = password,
            Age = age,
            PhoneNumber = phoneNumber,
            Address = address,
            CPF = cpf,
            Gender = gender,
            CreatedAt = DateTime.Now,
            IsActive = true,
        };
        switch (roles.Count == 0)
        {
            case true:
                user._roles.Add(Role.User);
                break;
            case false:
                foreach (var role in roles)
                {
                    user._roles.Add(role);
                }
                break;
        }
        return user;
    }
    
    public void UpdateUser(
        string name,
        string email,
        string password,
        DateTime age,
        int phoneNumber,
        string address,
        string cpf,
        bool gender)
    {
        Validation(name, email, password, age, cpf);

        Name = name;
        Email = email;
        Password = password;
        Age = age;
        PhoneNumber = phoneNumber;
        Address = address;
        CPF = cpf;
        Gender = gender;
        IsActive = true;
        UpdatedAt = DateTime.Now;
    }
    
    public void AddRole(Role role)
    {
        if (_roles.Any(r => r.Name == role.Name))
            return;

        _roles.Add(role);
    }

    public bool HasRole(Role role)
    {
        return _roles.Any(r => r.Name == role.Name);
    }

    
    public void SuspendUser(string? reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("The reason for suspension must be provided.");
        if (!IsActive)
            throw new DomainException("User is already suspended.");
        
        SuspensionReason = reason;
        IsActive = false;
        SuspendAt = DateTime.Now;
    }
    public void ReactivateUser()
    {
        if (IsActive)
            throw new DomainException("User is already active.");

        IsActive = true;
        SuspendAt = null;
        SuspensionReason = null;
    }
    
    private static bool Validation(string name, string email, string password, DateTime age,
        string cpf)
    {
        if (name.Length < 3)
            throw new DomainException("The name must have at least 3 characters.");
        
        if (email.Length > 256)
            throw new DomainException("The email must have a maximum of 256 characters.");
        
        if (!email.Contains('@') || !email.EndsWith(".com"))
            throw new DomainException("The email must contain '@' and end with '.com'.");
        
        var userAge = DateTime.Now.Year - age.Year;
        if (age > DateTime.Now.AddYears(-userAge)) userAge--;
        if (userAge < 15)
            throw new DomainException("Age must be greater than 15 years.");
        
        if (!IsValidCPF(cpf))
            throw new DomainException("Invalid CPF.");
        
        return true;
    }
    
    private static bool IsValidCPF(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpf.Length != 11) return false;
        if (cpf.Distinct().Count() == 1) return false;

        // Validação dos dígitos verificadores
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
    
    #endregion
}
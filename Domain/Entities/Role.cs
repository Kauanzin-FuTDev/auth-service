namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; } // "Admin", "User"
    public string Description { get; private set; }

    #region Constructor

    private Role()
    {
        
    }
    public Role(string name, string description)
    {
        Name = name;
        Description = description;
    }
    #endregion

    #region Methods

    public static readonly Role Admin = new Role(
        "Admin",
        "Full system access. Can manage users, orders, and configurations."
    );

    public static readonly Role User = new Role(
        "User",
        "Regular customer who can browse products and place orders."
    );
    
    public static readonly Role Support = new Role(
        "Support",
        "Provides assistance to users and handles service requests."
    );
    

    #endregion
}
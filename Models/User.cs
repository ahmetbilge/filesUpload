using System.Xml.Linq;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } =string.Empty;


    public string Password { get; set; } = string.Empty;// Hashleme eklenebilir

   /* public User()
    {
        Username = string.Empty;
        Password = string.Empty;
    }*/

}

namespace AccountService.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Note: Never store passwords as plain text in a real-world application
                                              // Include other fields as necessary
    }

}

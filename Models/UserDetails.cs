namespace UserManagement.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int IsActive { get; set; } 
}
}

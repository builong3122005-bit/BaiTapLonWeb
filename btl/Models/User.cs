namespace btl.Models
{
    public class User
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
        public string password { get; set; }

        //Them display va phone
        //public string DisplayName { get; set; }

        //public string PhoneNumBer { get; set; }

        private static int nextId = 1;

        public User()
        {
            this.id = nextId;
            nextId++;
        }

        public User(string fullname, string email, /* string phonenumber*/ string password, string role /*string displayname*/)
        {
            this.fullname = fullname;
            this.email = email;
            this.password = password;
            this.id = nextId;
            this.role = role;


            // them nay
            //this.PhoneNumBer = phonenumber;
            //this.DisplayName = displayname;

            nextId++;
        }
    }
}
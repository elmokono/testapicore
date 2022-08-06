using System.Linq;

namespace testapicore.Models.DummyDataDBInitializer
{
    public class DbInitializer
    {
        public static void Initialize(AWSTestDatabaseDBContext context)
        {
            //se asegura de que exista la db
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var userStatuses = new UserStatus[]
            {
                new UserStatus{ Id=1, Description="Active" },
                new UserStatus{ Id=2, Description="Suspended" },
                new UserStatus{ Id=99, Description="Archived" },
            };

            foreach (UserStatus u in userStatuses)
                context.UserStatuses.Add(u);

            var users = new User[]
            {
                new User{ Id=1, FullName="Cosme Fulanito", UserStatus =userStatuses.Single(x => x.Id==1) },
                new User{ Id=2, FullName="Pollo Viñolo", UserStatus =userStatuses.Single(x => x.Id==1) },
                new User{ Id=3, FullName="Cabra Belcebu", UserStatus =userStatuses.Single(x => x.Id==2) },
                new User{ Id=4, FullName="Chivo Berrinche", UserStatus =userStatuses.Single(x => x.Id==99) },
            };

            foreach (User u in users)
                context.Users.Add(u);

            context.SaveChanges();

        }
    }
}

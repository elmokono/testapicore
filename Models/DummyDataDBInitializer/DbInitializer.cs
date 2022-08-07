using System;
using System.Linq;

namespace testapinet6.Models.DummyDataDBInitializer
{
    public class DbInitializer
    {
        public static void Initialize(AWSTestDatabaseDBContext context)
        {
            context.Database.EnsureDeleted();

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
                new User{ /*Id=1,*/ FullName="Cosme Fulanito", Email="Cosme.Fulanito@user.com", UserStatus =userStatuses.Single(x => x.Id==1) },
                new User{ /*Id=2,*/ FullName="Pollo Viñolo", Email="Pollo.Vinolo@user.com", UserStatus =userStatuses.Single(x => x.Id==1) },
                new User{ /*Id=3,*/ FullName="Cabra Belcebu", Email="Cabra.Belcebu@user.com", UserStatus =userStatuses.Single(x => x.Id==2) },
                new User{ /*Id=4,*/ FullName="Chivo Berrinche", Email="Chivo.Berrinche@user.com", UserStatus =userStatuses.Single(x => x.Id==99) },
            };

            foreach (User u in users)
                context.Users.Add(u);

            var pacients = new Pacient[]
            {
                new Pacient{ /*Id=1,*/ FullName="Paciente 1", NationalId="12345678" },
                new Pacient{ /*Id=2,*/ FullName="Paciente 2", NationalId="91011121" },
                new Pacient{ /*Id=3,*/ FullName="Paciente 3", NationalId="31415161" },
                new Pacient{ /*Id=4,*/ FullName="Paciente 4", NationalId="71819202" },
            };

            foreach (Pacient u in pacients)
                context.Pacients.Add(u);

            var appointments = new Appointment[]
            {
                new Appointment{ /*Id=10,*/ Pacient=pacients[0], User=users[0], When=DateTime.Now, Confirmed=false },
                new Appointment{ /*Id=20,*/ Pacient=pacients[1], User=users[1], When=DateTime.Now, Confirmed=true },
                new Appointment{ /*Id=30,*/ Pacient=pacients[2], User=users[1], When=DateTime.Now, Confirmed=false },
                new Appointment{ /*Id=40,*/ Pacient=pacients[0], User=users[0], When=DateTime.Now, Confirmed=true },
            };

            foreach (Appointment u in appointments)
                context.Appointments.Add(u);

            context.SaveChanges();

        }
    }
}

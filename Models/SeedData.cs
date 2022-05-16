using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD_125_BugTracker.Data;

namespace SD_125_BugTracker.Models
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            List<string> roles = new List<string>
            {
                "Admin", "Project Manager", "Developer", "Submitter"
            };
            if ( !context.Roles.Any() )
            {
                foreach ( string role in roles )
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                context.SaveChanges();
            }
            if ( !context.Users.Any() )
            {
                //admin for demo feature
                ApplicationUser seededUser1 = new ApplicationUser
                {
                    Email = "demoadmin@test.ca",
                    NormalizedEmail = "DEMOADMIN@TEST.CA",
                    UserName = "demoadmin@test.ca",
                    NormalizedUserName = "DEMOADMIN@TEST.CA",
                    EmailConfirmed = true,
                };
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(seededUser1, "P@ssword1");
                seededUser1.PasswordHash = hashed;
                await userManager.CreateAsync(seededUser1);
                await userManager.AddToRoleAsync(seededUser1, "Admin");

                ApplicationUser seededUser2 = new ApplicationUser
                {
                    Email = "pm1@bugtracker.ca",
                    NormalizedEmail = "PM1@BUGTRACKER.CA",
                    UserName = "pm1@bugtracker.ca",
                    NormalizedUserName = "PM1@BUGTRACKER.CA",
                    EmailConfirmed = true,
                };

                var hashed2 = password.HashPassword(seededUser2, "P@ssword1");
                seededUser2.PasswordHash = hashed2;
                await userManager.CreateAsync(seededUser2);
                await userManager.AddToRoleAsync(seededUser2, "Project Manager");

                ApplicationUser seededUser3 = new ApplicationUser
                {
                    Email = "admin@bugtracker.ca",
                    NormalizedEmail = "ADMIN@BUGTRACKER.CA",
                    UserName = "admin@bugtracker.ca",
                    NormalizedUserName = "ADMIN@BUGTRACKER.CA",
                    EmailConfirmed = true,
                };
                var hashed3 = password.HashPassword(seededUser3, "P@ssword1");
                seededUser3.PasswordHash = hashed3;
                await userManager.CreateAsync(seededUser3);
                await userManager.AddToRoleAsync(seededUser3, "Admin");


                ApplicationUser seededUser4 = new ApplicationUser
                {
                    Email = "developer1@bugtracker.ca",
                    NormalizedEmail = "DEVELOPER1@BUGTRACKER.CA",
                    UserName = "developer1@bugtracker.ca",
                    NormalizedUserName = "DEVELOPER1@BUGTRACKER.CA",
                    EmailConfirmed = true,
                };
                var hashed4 = password.HashPassword(seededUser4, "P@ssword1");
                seededUser4.PasswordHash = hashed4;
                await userManager.CreateAsync(seededUser4);
                await userManager.AddToRoleAsync(seededUser4, "Developer");

                ApplicationUser seededUser5 = new ApplicationUser
                {
                    Email = "submitter1@bugtracker.ca",
                    NormalizedEmail = "SUBMITTER1@BUGTRACKER.CA",
                    UserName = "submitter1@bugtracker.ca",
                    NormalizedUserName = "SUBMITTER1@BUGTRACKER.CA",
                    EmailConfirmed = true,
                };
                var hashed5 = password.HashPassword(seededUser5, "P@ssword1");
                seededUser5.PasswordHash = hashed5;
                await userManager.CreateAsync(seededUser5);
                await userManager.AddToRoleAsync(seededUser5, "Submitter");
            }
            await context.SaveChangesAsync();
           // add priority
            if (!context.TicketPriorities.Any())
            {
                TicketPriority ticketPriority1 = new TicketPriority();
                ticketPriority1.Name = Priority.High;

                TicketPriority ticketPriority2 = new TicketPriority();
                ticketPriority2.Name = Priority.Medium;

                TicketPriority ticketPriority3 = new TicketPriority();
                ticketPriority3.Name = Priority.Low;

                context.TicketPriorities.Add(ticketPriority1);
                context.TicketPriorities.Add(ticketPriority2);
                context.TicketPriorities.Add(ticketPriority3);
                context.SaveChanges();
            }
            //add ticketstatus
            if (!context.TicketStatuses.Any())
            {
                TicketStatus ticketStatus1 = new TicketStatus();
                ticketStatus1.Status = Status.Completed;

                TicketStatus ticketStatus2 = new TicketStatus();
                ticketStatus2.Status = Status.InProgress;

                TicketStatus ticketStatus3 = new TicketStatus();
                ticketStatus3.Status = Status.Assigned;

                TicketStatus ticketStatus4 = new TicketStatus();
                ticketStatus4.Status = Status.Unassigned;

                context.TicketStatuses.Add(ticketStatus1);
                context.TicketStatuses.Add(ticketStatus2);
                context.TicketStatuses.Add(ticketStatus3);
                context.TicketStatuses.Add(ticketStatus4);
                context.SaveChanges();
            }
            // add tickettype
            if (!context.TicketyTypes.Any())
            {
                TicketType ticketType1 = new TicketType();
                ticketType1.Name = Type.InformationRequest;

                TicketType ticketType2 = new TicketType();
                ticketType2.Name = Type.ServiceRequest;

                TicketType ticketType3 = new TicketType();
                ticketType3.Name = Type.Incident;

                context.TicketyTypes.Add(ticketType1);
                context.TicketyTypes.Add(ticketType2);
                context.TicketyTypes.Add(ticketType3);
                context.SaveChanges();
            }
        }
    }
}


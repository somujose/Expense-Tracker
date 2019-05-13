namespace ExpenseTracker.Repository.Migrations
{
    using ExpenseTracker.Model.EF;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseTracker.Repository.ExpenseTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExpenseTracker.Repository.ExpenseTrackerContext context)
        {
            var defaultGroup = new ExpenseGroup() { Name = "Europian trip" };
            context.ExpenseGroups.Add(defaultGroup);

            var userList = new List<ExpenseUser>
            {
                new ExpenseUser() { Email = "somujose@gmail.com", DisplayName = "Somu Jose" },
                new ExpenseUser() { Email = "dipu@gmail.com", DisplayName = "Dipu" },
                new ExpenseUser() { Email = "jahan@gmail.com", DisplayName = "Jahan" },
                new ExpenseUser() { Email = "jinoy@gmail.com", DisplayName = "Jinoy" }
            };
            context.ExpenseUsers.AddRange(userList);
            context.SaveChanges();
            var groupId = context.ExpenseGroups.FirstOrDefault().Id;
            var user1Id = context.ExpenseUsers.FirstOrDefault(x=>x.Email == "somujose@gmail.com").Id;
            var user2Id = context.ExpenseUsers.FirstOrDefault(x => x.Email == "dipu@gmail.com").Id;
            var user3Id = context.ExpenseUsers.FirstOrDefault(x => x.Email == "jahan@gmail.com").Id;
            var user4Id = context.ExpenseUsers.FirstOrDefault(x => x.Email == "jinoy@gmail.com").Id;


            var expenseItems = new List<ExpenseItem>
            {
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 120, UserId = user1Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 46, UserId = user1Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 80, UserId = user2Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 55, UserId = user3Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 110, UserId = user3Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 66, UserId = user3Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 60, UserId = user4Id },
                new ExpenseItem() { ExpenseGroupId = groupId, Amount = 35, UserId = user4Id }
            };

            context.ExpenseItems.AddRange(expenseItems);
            context.SaveChanges();
        }
    }
}

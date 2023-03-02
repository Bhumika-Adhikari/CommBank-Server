using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommBank.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CommBank_Server.Data
{
    public class SeedData
    {
        private readonly IMongoDatabase mongoDatabase;


        public SeedData(IMongoDatabase database)
        {
            mongoDatabase = database;
            SeedAllData();
        }

        public async void SeedAllData()
        {
            var SeedAccountsTask = SeedAccounts();
            var SeedGoalsTask = SeedGoals();
            var SeedTagsTask = SeedTags();
            var SeedTransactionsTask = SeedTransactions();
            var SeedUsersTask = SeedUsers();
            List<Task> seedDataTasks = new List<Task> { SeedAccountsTask, SeedGoalsTask, SeedTagsTask, SeedTransactionsTask, SeedUsersTask };
            await Task.WhenAll(seedDataTasks);
        }

        public async Task SeedAccounts()
        {
            var jsontext = await System.IO.File.ReadAllTextAsync(@"SeedData/Data/Accounts.json");
            var _accounts = BsonSerializer.Deserialize<List<Account>>(jsontext);
            if (_accounts != null)
            {

                IMongoCollection<Account> _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
                await _accountsCollection.InsertManyAsync(_accounts);
            }
        }
        public async Task SeedGoals()
        {
            var jsontext = await System.IO.File.ReadAllTextAsync(@"SeedData/Data/Goals.json");
            var _goals = BsonSerializer.Deserialize<List<Goal>>(jsontext);
            if (_goals != null)
            {
                IMongoCollection<Goal> _collection = mongoDatabase.GetCollection<Goal>("Goals");
                await _collection.InsertManyAsync(_goals);
            }
        }

        public async Task SeedTags()
        {
            var jsontext = await System.IO.File.ReadAllTextAsync(@"SeedData/Data/Tags.json");
            var tags = BsonSerializer.Deserialize<List<CommBank.Models.Tag>>(jsontext);
            if (tags != null)
            {
                IMongoCollection<CommBank.Models.Tag> _collection = mongoDatabase.GetCollection<CommBank.Models.Tag>("Tags");
                await _collection.InsertManyAsync(tags);
            }
        }

        public async Task SeedTransactions()
        {
            var jsontext = await System.IO.File.ReadAllTextAsync(@"SeedData/Data/Transactions.json");
            var transactions = BsonSerializer.Deserialize<List<Transaction>>(jsontext);
            if (transactions != null)
            {
                IMongoCollection<Transaction> _collection = mongoDatabase.GetCollection<Transaction>("Transactions");
                await _collection.InsertManyAsync(transactions);
            }
        }

        public async Task SeedUsers()
        {
            var jsontext = await System.IO.File.ReadAllTextAsync(@"SeedData/Data/Users.json");
            var users = BsonSerializer.Deserialize<List<User>>(jsontext);
            if (users != null)
            {
                IMongoCollection<User> _collection = mongoDatabase.GetCollection<User>("Users");
                await _collection.InsertManyAsync(users);
            }
        }

    }
}
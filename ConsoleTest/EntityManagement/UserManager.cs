using System.Collections.Generic;
using Azure.Models;

namespace Azure.EntityManagement
{
    public class UserManager : AzureManager
    {
        private const string UserCollection = "Users";

        public UserManager(string endpoint, string key) : base(endpoint, key)
        {
        }

        public List<User> GetUsers()
        {
            return Cosmos.GetDocuments<User>(UserCollection, x => true);
        }
    }
}
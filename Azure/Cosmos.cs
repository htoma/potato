using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace Azure
{
    public class Cosmos
    {
        private readonly DocumentClient _client;
        private readonly string _dbName;

        public Cosmos(string endpoint, string key, string dbName)
        {
            _client = new DocumentClient(new Uri(endpoint), key);
            _dbName = dbName;
        }

        public async Task<T> GetDocument<T>(string collection, string id) where T : class
        {
            try
            {
                var doc = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_dbName, collection, id));
                return JsonConvert.DeserializeObject<T>(doc.Resource.ToString());
            }
            catch (Exception ex)
            {
                // todo: log exception
                return null;
            }
        }

        public List<T> GetDocuments<T>(string collection, Func<T, bool> filter) where T : class
        {
            try
            {
                FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
                return _client
                    .CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(_dbName, collection),
                        queryOptions).Where(x => filter(x)).ToList();
            }
            catch (Exception ex)
            {
                // todo: log exception
                return null;
            }
        }

        public async Task<bool> CreateDocument<T>(string collection, T doc)
        {
            try
            {
                await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _dbName });
                await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_dbName),
                    new DocumentCollection { Id = collection });
                await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dbName, collection), doc);
                return true;
            }
            catch (Exception ex)
            {
                // todo: log exception
                return false;
            }
        }

        public async Task<bool> UpdateDocument<T>(string collection, string id, T doc)
        {
            try
            {
                await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_dbName, collection, id), doc);
                return true;
            }
            catch (Exception ex)
            {
                // todo: log exception
                return false;
            }
        }

        public async Task<bool> DeleteDocument(string collection, string id)
        {
            try
            {
                await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_dbName, collection, id));
                return true;
            }
            catch (Exception ex)
            {
                // todo: log exception
                return false;
            }
        }

        //static void Main(string[] args)
        //{
        //    var endpoint = "https://senorpotato.documents.azure.com:443/";
        //    var key = "4neoP9k7h2QePysXgfqZKYPJLD4ui3eAu1PHDg093B8SQwcwkoI0mZD0G1l1vouu07ZGhOebc9fFGvRfeejnGQ==";

        //    var 
        //    var dbName = "Potato";
        //    var usersCollection = "Users";
        //    var teamA = new Team
        //    {
        //        Name = "A team"
        //    };
        //    var teamB = new Team
        //    {
        //        Name = "B team"
        //    };
        //    var userA = new Data.Models.User
        //    {
        //        Name = "User A",
        //        Teams = new List<Team> {teamA}
        //    };
        //    var userB = new Data.Models.User
        //    {
        //        Name = "User B",
        //        Teams = new List<Team> { teamB }
        //    };

        //    var a = CreateDocument(client, dbName, usersCollection, userA).Result;
        //    var b = CreateDocument(client, dbName, usersCollection, userB).Result;

        //    FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
        //    var user = client
        //        .CreateDocumentQuery<Data.Models.User>(UriFactory.CreateDocumentCollectionUri(dbName, usersCollection),
        //            queryOptions).Where(x => x.Name == "User A").AsEnumerable().First();

        //    //var user = ReadDocument<Data.Models.User>(client, dbName, usersCollection, queryResult.Id.ToString()).Result;
        //    user.Name = "User A revisited";
        //    var updated = UpdateDocument(client, dbName, usersCollection, user.Id.ToString(), user).Result;

        //    user = client
        //        .CreateDocumentQuery<Data.Models.User>(UriFactory.CreateDocumentCollectionUri(dbName, usersCollection),
        //            queryOptions).Where(x => x.Name == "User B").AsEnumerable().First();
        //    var deleted = DeleteDocument(client, dbName, usersCollection, user.Id.ToString()).Result;
        //}                      
    }
}

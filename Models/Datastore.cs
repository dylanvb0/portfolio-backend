using Google.Cloud.Datastore.V1;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace portfolio_backend.Models
{
    public class Datastore <T> where T : DSObject
    {
        private readonly string _projectId;
        private readonly DatastoreDb _db;
        private readonly string _kind;

        /// <summary>
        /// Create a new datastore-backed bookstore.
        /// </summary>
        /// <param name="projectId">Your Google Cloud project id</param>
        public Datastore(string kind, string namespce = "")
        {
            _projectId = "dylans-test-project";
            _db = DatastoreDb.Create(_projectId, namespce);
            _kind = kind;
        }

        // [START create]
        public void Create(T item)
        {
            var entity = item.ToEntity(null);
            entity.Key = _db.CreateKeyFactory(_kind).CreateIncompleteKey();
            var keys = _db.Insert(new[] { entity });
            item.Id = keys.First().Path.First().Id;
        }
        // [END create]

        public void Delete(long id)
        {
            _db.Delete(ToKey(id));
        }

        // [START list]
        public IEnumerable<T> List()
        {
            var query = new Query(_kind);
            var results = _db.RunQuery(query);
            return results.Entities.Select(entity => ToObject(entity));
        }
        // [END list]

        public T Read(long id)
        {
            var result = _db.Lookup(ToKey(id));
            return result != null ? ToObject(result) : null;
        }

        public void Update(T item)
        {
            _db.Update(item.ToEntity(_db.CreateKeyFactory(_kind)));
        }
         public T ToObject(Entity entity)
         {
           return (T)Activator.CreateInstance(typeof(T), entity);
         }
        public Key ToKey (long id){
          return _db.CreateKeyFactory(_kind).CreateKey(id);
        }
    }
}

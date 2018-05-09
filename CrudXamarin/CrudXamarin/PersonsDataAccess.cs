using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System;

namespace CrudXamarin
{
   public class PersonsDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Person> Persons { get; set; }


        public PersonsDataAccess()
        {
            database =
              DependencyService.Get<IDatabaseConnection>().
              DbConnection();
            database.CreateTable<Person>();
            this.Persons =
              new ObservableCollection<Person>(database.Table<Person>());
            // If the table is empty, initialize the collection
            if (!database.Table<Person>().Any())
            {
                AddNewPerson();
            }
        }


        public void AddNewPerson()
        {
            this.Persons.
              Add(new Person
              {
                  FirstName = "First name...",
                  LastName = "Last name...",
                  Gender = "Gender...",
                  Location = "Location...",
                  DateOfBirth = DateTime.Now,
                  Images= "Image..."
                 
              });
        }

        public IEnumerable<Person> GetFilteredPersons(string firstName)
        {
            lock (collisionLock)
            {
                var query = from pers in database.Table<Person>()
                            where pers.FirstName == firstName
                            select pers;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<Person> GetFilteredPersons()
        {
            lock (collisionLock)
            {
                return database.Query<Person>(
                  "SELECT * FROM Item WHERE  FirstName  = 'Richard'").AsEnumerable();
            }
        }


       
        public Person GetPerson(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Person>().
                  FirstOrDefault(person => person.Id == id);
            }
        }
        public int SavePerson(Person personInstance)
        {
            lock (collisionLock)
            {
                if (personInstance.Id != 0)
                {
                    database.Update(personInstance);
                    return personInstance.Id;
                }
                else
                {
                    database.Insert(personInstance);
                    return personInstance.Id;
                }
            }
        }
        public void SaveAllPersons()
        {
            lock (collisionLock)
            {
                foreach (var personInstance in this.Persons)
                {
                    if (personInstance.Id != 0)
                    {
                        database.Update(personInstance);
                    }
                    else
                    {
                        database.Insert(personInstance);
                    }
                }
            }
        }
        public int DeletePerson(Person personInstance)
        {
            var id = personInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Person>(id);
                }
            }
            this.Persons.Remove(personInstance);
            return id;
        }
        public void DeleteAllPersons()
        {
            lock (collisionLock)
            {
                database.DropTable<Person>();
                database.CreateTable<Person>();
            }
            this.Persons = null;
            this.Persons = new ObservableCollection<Person>
              (database.Table<Person>());
        }
    }
}

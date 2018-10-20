using SQLite;

using System;

using System.ComponentModel;

namespace CrudXamarin
{
    
    [Table("Persons")]
    public class Person : INotifyPropertyChanged
    {
        
        private int _id;
        
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            
            get
            {
                return _id;
            }
            
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        
        }
        
        private string _firstName;
        
        [NotNull]
        public string FirstName
        {
            
            get
            {
                return _firstName;
            }
            
            set
            {
                this._firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        
        }

        
        private string _lastName;
      
        
        public string LastName
        {
            
            get
            {
                return _lastName;
            }
            
            set
            {
                this._lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        
        }
        
        private string _location;
        
        [MaxLength(50)]
        public string Location
        {
            get
            {
                return _location;
            }
            
            set
            {
                this._location = value;
                OnPropertyChanged(nameof(Location));
            }
        
        }
        
        private string _gender;
        
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                this._gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private DateTime _dateOfBirth;
        
        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                this._dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        private string _image;
        public string Images
        {
            get
            {
                return _image;
            }
            set
            {
                this._image = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}

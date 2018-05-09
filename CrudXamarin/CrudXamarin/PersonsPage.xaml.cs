using System;
using System.Linq;
using Xamarin.Forms;


using Plugin.Media;
using Plugin.Media.Abstractions;


namespace CrudXamarin
{
    public partial class PersonsPage : ContentPage
    {
        private PersonsDataAccess dataAccess;
        public PersonsPage()
        {
            InitializeComponent();
            // An instance of the PersonsDataAccessClass
            // that is used for data-binding and data access
            this.dataAccess = new PersonsDataAccess();

            //Button pickPhoto = new Button();
            //Image image = new Image();

          

        }


        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,

                });


                if (file == null)
                    return;
            Image image = new Image
            {
                Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                })
            };
      
        }

        // An event that is raised when the page is shown
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // The instance of PersonsDataAccess
            // is the data binding source
            this.BindingContext = this.dataAccess;
        }
        // Save any pending changes
        private void OnSaveClick(object sender, EventArgs e)
        {
            this.dataAccess.SaveAllPersons();
        }
        // Add a new Person to the Persons collection
        private void OnAddClick(object sender, EventArgs e)
        {
            this.dataAccess.AddNewPerson();
        }
        // Remove the current Person
        // If it exist in the database, it will be removed
        // from there too
        private void OnRemoveClick(object sender, EventArgs e)
        {
            if (this.MyListView.SelectedItem is Person currentPerson)
            {
                this.dataAccess.DeletePerson(currentPerson);
            }
        }
        // Remove all persons
        // Use a DisplayAlert object to ask the user's confirmation
        private async void OnRemoveAllClick(object sender, EventArgs e)
        {
            if (this.dataAccess.Persons.Any())
            {
                var result =
                  await DisplayAlert("Confirmation",
                  "Are you sure? This cannot be undone",
                  "OK", "Cancel");
                if (result == true)
                {
                    this.dataAccess.DeleteAllPersons();
                    this.BindingContext = this.dataAccess;
                }
            }
        }
    }
}
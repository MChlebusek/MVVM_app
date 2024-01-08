using MVVM_app.Models;
using MVVM_app.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MVVM_app.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using System.Reflection;


namespace MVVM_app.ViewModels


{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<FriendViewModel> Friends { get; set; }

        public ICommand CreateFriendCommand {get; protected set; }

        public ICommand DeleteFriendCommand { get; protected set; }

        public ICommand SaveFriendCommand { get; protected set; }

        public ICommand BackCommand { get; protected set; }

        public ICommand AddPhotoCommand { get; protected set; }


        FriendViewModel selectedFriend;


        public INavigation Navigation { get; set; }

        public FriendsListViewModel()
        {
            Friends = new ObservableCollection<FriendViewModel>();
            CreateFriendCommand = new Command(CreateFriend);

            DeleteFriendCommand = new Command(DeleteFriend);

            SaveFriendCommand = new Command(SaveFriend);  

            BackCommand = new Command(Back);

            AddPhotoCommand = new Command(async () => await SelectPhotoAsync());




        }

        public async Task SelectPhotoAsync()
        {
            try
            {
                var options = new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Select a photo"
                };

                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var bytes = new byte[stream.Length];
                    await stream.ReadAsync(bytes, 0, (int)stream.Length);

                    // Set the selected photo to the Photo property
                    selectedFriend.Photo = bytes;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error picking photo: {ex.Message}");
            }
        }

        public static Stream GetImageStream(string imageName)
        {
            try
            {
                var assembly = typeof(FriendsListViewModel).GetTypeInfo().Assembly;
                return assembly.GetManifestResourceStream($"MVVM_app.pictures.{imageName}");
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"Error getting image stream: {ex.Message}");
                return null;
            }
        }



        protected void OnPopertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));

            }
        }



        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    FriendViewModel tempFriend = value;
                    selectedFriend = null;
                    OnPopertyChanged("SelectedFriend");
                    Navigation.PushAsync(new FriendPage(tempFriend));
                }
            }


        }

        private void SaveFriend(object friendobj)
        {
            FriendViewModel friend = (FriendViewModel)friendobj;
            if (friend != null && friend.IsValid && !Friends.Contains(friend))
            {
                Friends.Add(friend);


            }
            Back();

        }

        public void Back()
        {
            Navigation.PopAsync();
        }

        private void DeleteFriend(object friendobj)
        {
            FriendViewModel friend = (FriendViewModel)friendobj;
            if (friend != null)
            {
                Friends.Remove(friend);
                Back();

            }
        }

        public void CreateFriend()
        {
            Navigation.PushAsync(new FriendPage(new FriendViewModel() { ListViewModel = this }));
        }

        
        }
    }

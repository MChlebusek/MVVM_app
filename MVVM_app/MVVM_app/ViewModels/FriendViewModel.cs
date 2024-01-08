using MVVM_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MVVM_app.ViewModels
{
    public class FriendViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        FriendsListViewModel lvm;

        public Friend Friend { get; private set; }

        public FriendViewModel()
        {
            Friend = new Friend();
        }

        public FriendsListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPopertyChanged("ListViewModel");
                }
            }
        }

        public string Name
        {
            get { return Friend.Name; }
            set
            {
                if (Friend.Name != value)
                {
                    Friend.Name = value;
                    OnPopertyChanged("Name");
                }
            }
        }

        public string Email
        {
            get { return Friend.Email; }
            set
            {
                if (Friend.Email != value)
                {
                    Friend.Email = value;
                    OnPopertyChanged("Email");
                }
            }
        }

        public string Phone
        {
            get { return Friend.Phone; }
            set
            {
                if (Friend.Phone != value)
                {
                    Friend.Phone = value;
                    OnPopertyChanged("Phone");
                }
            }
        }
        public int Age
        {
            get { return Friend.Age; }
            set
            {
                if (Friend.Age != value)
                {
                    Friend.Age = value;
                    OnPopertyChanged("Age");
                }
            }
        }



        public byte[] Photo
        {
            get { return Friend.Photo; }
            set
            {
                if (Friend.Photo != value)
                {
                    Friend.Photo = value;
                    OnPopertyChanged("Photo"); 
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(Name.Trim()) ||
                        !string.IsNullOrEmpty(Phone.Trim()) ||
                        !string.IsNullOrEmpty(Email.Trim())) ||
                        (Photo != null && Photo.Length > 0);
            }
        }

        public void OnPopertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public async Task SelectPhotoAsync(object friend)
        {
            FriendViewModel selectedFriend = friend as FriendViewModel;
            if (selectedFriend != null)
            {
                // Print some debug info
                Console.WriteLine($"SelectPhotoAsync: FriendViewModel Name = {selectedFriend.Name}");

                try
                {
                    var mediaResult = await MediaPicker.PickPhotoAsync();
                    if (mediaResult != null)
                    {
                        // Read the stream to get the byte array
                        using (var stream = await mediaResult.OpenReadAsync())
                        {
                            var bytes = new byte[stream.Length];
                            await stream.ReadAsync(bytes, 0, (int)stream.Length);

                            // Set the selected photo to the Photo property of the selectedFriend
                            selectedFriend.Photo = bytes;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine($"Error picking photo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"SelectPhotoAsync: friend parameter is not a FriendViewModel. Type: {friend?.GetType().FullName}");
            }
        }




    }
}

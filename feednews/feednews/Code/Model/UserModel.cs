using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace feednews.Code.Model
{
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }

        private string _id;

        public string _Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        private PostModel[] posts;

        public PostModel[] Posts
        {
            get { return posts; }
            set { posts = value; OnPropertyChanged(); }
        }

        private bool isbusy = false;

        public bool IsBusy
        {
            get { return isbusy = false; }
            set { isbusy = value; OnPropertyChanged(); }
        }
    }
}

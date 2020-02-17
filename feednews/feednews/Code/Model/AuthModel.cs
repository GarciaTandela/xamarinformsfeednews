using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace feednews.Code.Model
{
    public class AuthModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string nome = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }

        private string token;

        public string Token
        {
            get { return token; }
            set { token = value; OnPropertyChanged(); }
        }

        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; OnPropertyChanged(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace feednews.Code.Model
{
    public class PostModel : INotifyPropertyChanged
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

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }


        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; OnPropertyChanged(); }
        }

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get
            {
                if (imageSource == null)
                {
                    imageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(ImageUrl)));

                };
                return imageSource;
            }

            set { imageSource = value; OnPropertyChanged(); }

        }

        private UserModel creator;

        public UserModel Creator
        {
            get { return creator; }
            set { creator = value; OnPropertyChanged(); }
        }

        private string createdAt;

        public string CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; OnPropertyChanged(); }
        }

        private string updatedAt;

        public string UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; OnPropertyChanged(); }
        }

        private bool isbusy = false;

        public bool IsBusy
        {
            get { return isbusy = false; }
            set { isbusy = value; OnPropertyChanged(); }
        }

        //=====================================================================
        //Properties for creating post
        public class InsertPost
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

            private string title;

            public string Title
            {
                get { return title; }
                set { title = value; OnPropertyChanged(); }
            }

            private string content;

            public string Content
            {
                get { return content; }
                set { content = value; OnPropertyChanged(); }
            }


            private string imageUrl;

            public string ImageUrl
            {
                get { return imageUrl; }
                set { imageUrl = value; OnPropertyChanged(); }
            }

            private ImageSource imageSource;

            public ImageSource ImageSource
            {
                get
                {
                    if (imageSource == null)
                    {
                        imageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(ImageUrl)));

                    };
                    return imageSource;
                }

                set { imageSource = value; OnPropertyChanged(); }

            }

            private UserModel creator;

            public UserModel Creator
            {
                get { return creator; }
                set { creator = value; OnPropertyChanged(); }
            }

            private string createdAt;

            public string CreatedAt
            {
                get { return createdAt; }
                set { createdAt = value; OnPropertyChanged(); }
            }

            private string updatedAt;

            public string UpdatedAt
            {
                get { return updatedAt; }
                set { updatedAt = value; OnPropertyChanged(); }
            }

            private bool isbusy = false;

            public bool IsBusy
            {
                get { return isbusy = false; }
                set { isbusy = value; OnPropertyChanged(); }
            }
        }

        private ObservableCollection<InsertPost>  posts;

        public ObservableCollection<InsertPost> Posts
        {
            get { return posts; }
            set { posts = value; OnPropertyChanged(); }
        }


    }
}

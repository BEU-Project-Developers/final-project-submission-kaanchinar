﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MusicAppWPF.Models;
using MusicAppWPF.Views;

namespace MusicAppWPF.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _errorMessage;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
        
    }
    
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public ICommand LoginCommand => new RelayCommand(Login);

    private void Login()
    {
        var hashedPassword = PasswordHelper.HashPassword(Password);
        var user = new User(Username, hashedPassword, null)
        {
            Username = Username,
            Password = hashedPassword
        };
        if (user.VerifyCredentials())
        {
            var welcomePage = new Welcome();
            if (Application.Current.MainWindow != null)
            {
                var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
                mainFrame?.Navigate(welcomePage);
            }
        }
        else
        {
            ErrorMessage = "Invalid username or password";
        }
    }


}
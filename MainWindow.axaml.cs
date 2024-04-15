using System.Collections.Generic;
using Avalonia.Controls;
using MySql.Data.MySqlClient;

namespace SuperBasketball;

public partial class MainWindow : Window {
    private string _connstring = "server=10.10.1.24; database=pro1_21; port3306; user Id=user_01; password=user02pro";
    private List<Gamer> _gamers;
    private MySqlConnection _connection;
    private string fullTable = "select ID, Surname, Position, Weight, Height, Birthday, Start, Team from Gamer";
    public MainWindow() {
        InitializeComponent();
    }

    public void ShowTable(string sql) {
        _gamers = new List<Gamer>();
        _connection = new MySqlConnection(_connstring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows) {
            var currentGamer = new Gamer() {
                ID = reader.GetInt32("ID"),
                Surname = reader.GetString("Surname"),
                Position = reader.GetString("Position"),
                Weight = reader.GetFloat("Weight"),
                Height = reader.GetFloat("Height"),
                Birthday = reader.GetDateTime("Birthday"),
                Start = reader.GetDateTime("Start"),
                Team = reader.GetString("Team"),
            };
            _gamers.Add(currentGamer);
        }
        _connection.Close();
        СуперБаскетбол.ItemsSource = _gamers;
    }

    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e) {
        string searchSql =
            "select ID, Surname, Position, Weight, Height, Birthday, Start, Team from Gamer where Surname like '%'" +
            txtSearch.Text + "%'";
    }
    
}
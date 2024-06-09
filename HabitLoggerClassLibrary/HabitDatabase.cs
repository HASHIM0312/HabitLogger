namespace HabitLoggerClassLibrary;

using System.Data.SQLite;

public class HabitDatabase
{

    public HabitDatabase()
    {

        conn = new SQLiteConnection(@"Data Source=habit-tracker.db");

        //Create database and table
        conn.Open();
        SQLiteCommand cmd = conn.CreateCommand();

        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS habits (id INTEGER PRIMARY KEY, name TEXT,  date TEXT, quantity INTEGER)";
        cmd.ExecuteNonQuery();

        conn.Close();


    }

    SQLiteConnection conn;


    public void InsertHabit(string? name, string? date, int? quantity)
    {
        conn.Open();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = @$"INSERT INTO habits (name, date, quantity) VALUES (@name, @date, @quantity);";
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@date", date);
        cmd.Parameters.AddWithValue("@quantity", quantity);
        cmd.ExecuteNonQuery();
        conn.Close();
    }


    public void GetHabits()
    {

        conn.Open();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM habits";
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"\nid: {reader.GetInt32(0).ToString()} | name: {reader.GetString(1).ToString()} | date: {reader.GetString(2).ToString()}" +
                $" | quantity: {reader.GetInt32(3).ToString()}\n");

        }
        conn.Close();


    }


    public bool UpdateHabit(int id, string? name, string? date, int quantity = 0)
    {
        if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(date) && quantity == 0)
        {
            Console.WriteLine("\n---HABIT REMAINS SAME----  Going back to menu...\n");
            return true;
        }

        conn.Open();
        SQLiteCommand cmd = conn.CreateCommand();

        //Sequentially update the name, date, and quantity using the index number *TO DO* 
        if (name != string.Empty)
        {
            cmd.CommandText = $"UPDATE habits SET name = @name WHERE id = @id;";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

        }
        if (date != string.Empty)
        {
            cmd.CommandText = $"UPDATE habits SET date = @name WHERE id = @id;";
            cmd.Parameters.AddWithValue("@name", date);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

        }
        if (quantity != 0)
        {
            cmd.CommandText = $"UPDATE habits SET quantity = @name WHERE id = @id;";
            cmd.Parameters.AddWithValue("@name", quantity);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

        }


        conn.Close();
        return false;



    }


    public void BreakHabit(int id)
    {

        conn.Open();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"DELETE FROM habits WHERE id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"UPDATE habits SET id = id - 1 WHERE id > @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        conn.Close();

    }
}




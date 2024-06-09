using System.Text.RegularExpressions;

bool endApp = false;


Console.WriteLine("Welcome to The Habit Tracker!");

HabitLoggerClassLibrary.HabitDatabase habitDatabase = new HabitLoggerClassLibrary.HabitDatabase();

while (!endApp)
{
    Console.WriteLine("---MAIN MENU---\n\nWhat would you like to do?\n\n  QUIT: '0'\n  REVIEW RECORDS: '1'" +
        "\n  INSERT HABIT: '2'\n  UPDATE HABIT: '3'\n  DELETE HABIT: '4'");

    //Input can be null-- exception *TO DO*
    Console.Write("\n--INPUT ('t' for Instructions): ");

    int input;
    {
        string? inputStr = Console.ReadLine();

        while (!int.TryParse(inputStr, out input) || input < 0)
        {
            if (inputStr.ToLower() == "t")
            {
                ShowInstructions();
                Console.Write("\n--INPUT ('T' for Instructions): ");
                inputStr = Console.ReadLine();
                continue;
            }
            Console.Write("\nPlease enter a valid integer > 0 ('T' for instructions): ");
            inputStr = Console.ReadLine();


        }
    }

    switch (input)
    {
        case 0:
            {
                endApp = true; break;
            }
        case 1:
            {
                Console.WriteLine("\n------START------");
                habitDatabase.GetHabits();
                Console.WriteLine("-------END------\n\n");
                //GetHabits
                break;
            }
        case 2:
            {
                //Ask for name of habit, date of habit, and quantity *TO DO* LOTS OF EXCEPTIONS HERE
                Console.Write("\n--INSERT HABIT--\nPlease enter the name: ");
                //Make sure name is not null

                string? name = Console.ReadLine();
                while (String.IsNullOrEmpty(name))
                {
                    Console.Write("\nPlease enter a valid name: ");
                    name = Console.ReadLine();
                }
                Console.Write("Please enter the date (mm/DD/yyyy): ");
                string? date = Console.ReadLine();
                var r = new Regex("^([0][1-9]|[1][0-2])([-.\\/])([0][1-9]|[1-2][0-9]|[3][0-1])\\2([0-9]{4})$");

                //Make sure date is in correct format (mm/DD/yyyy). Use the Regex pattern in order to check.
                //Used Gemini AI and https://stackoverflow.com/questions/10636284/validate-string-based-on-a-format
                //to understand Regex expressions
                while (date != null && !r.IsMatch(date))
                {
                    Console.Write("\nPlease enter the date in the correct format (mm/DD/yyyy): ");
                    date = Console.ReadLine();
                }

                //Make sure quantity is an integer. Copied the code from above to validate the input.
                Console.Write("Please enter the quantity of the habit done: ");

                int quantity;
                {
                    string? inputStr = Console.ReadLine();

                    //I used OR because an input of -1 will be parsed but it is still not valid
                    while (!int.TryParse(inputStr, out quantity) || quantity <= 0)
                    {
                        Console.Write("\nPlease enter a valid integer > 0: ");
                        inputStr = Console.ReadLine();
                    }
                }

                Console.WriteLine("\n");
                habitDatabase.InsertHabit(name, date, quantity);
                Console.WriteLine($"\nTHE FOLLOWING HABIT HAS BEEN INSERTED--\nname: {name} | date: {date}" +
                    $" | quantity: {quantity}\n");

                break;
            }


        case 3:
            {

                Console.Write("\n--UPDATE HABIT--\nPlease enter the id of the habit you would like to update\n('-1' to review records): ");
                string? inputID = Console.ReadLine();
                int id;


                while (!int.TryParse(inputID, out id) || id == -1)
                {
                    if (id == -1)
                    {
                        Console.WriteLine("\n----REVIEW OF RECORDS START----");
                        habitDatabase.GetHabits();
                        Console.WriteLine("\n----REVIEW OF RECORDS END----");
                        Console.Write("\n--UPDATE HABIT--\nPlease enter the id of the habit you would like to update ('-1' to review records): ");
                        inputID = Console.ReadLine();
                        continue;
                    }
                    Console.Write("\nPlease enter a valid integer ('-1' to review records): ");
                    inputID = Console.ReadLine();
                }
                Console.Write("Please enter the updated name (press Enter to keep it same): ");
                string? name = Console.ReadLine();
                Console.Write("Please enter the updated date (mm/DD/yyyy) (press Enter to keep it same): ");
                string? date = Console.ReadLine();
                Console.Write("Please enter the updated quantity of the habit done (press Enter to keep it same): ");
                string? quantityInput = Console.ReadLine();

                int quantity;

                if (String.IsNullOrEmpty(quantityInput))
                    quantity = 0;
                else
                {

                    while (!int.TryParse(quantityInput, out quantity))
                    {


                        Console.Write("\nPlease enter a valid integer: ");
                        quantityInput = Console.ReadLine();

                        if (String.IsNullOrEmpty(quantityInput))
                        {
                            quantity = 0;
                            break;

                        }
                    }
                }
                Console.WriteLine("\n");
                bool noUpdate = habitDatabase.UpdateHabit(id, name, date, quantity);
                if (!noUpdate)
                    Console.WriteLine($"\nHABIT HAS BEEN UPDATED-- REVIEW RECORDS TO SEE UPDATES");
                //UpdateHabit()
                break;
            }
        case 4:
            {
                Console.WriteLine("\n---BREAK HABIT---");
                Console.Write("Please enter the id of the habit you would like to break\n('-1' to review records): ");
                string? inputID = Console.ReadLine();
                int id;


                while (!int.TryParse(inputID, out id) || id == -1)
                {
                    if (id == -1)
                    {
                        Console.WriteLine("\n----REVIEW OF RECORDS START----");
                        habitDatabase.GetHabits();
                        Console.WriteLine("\n----REVIEW OF RECORDS END----");
                        Console.Write("\n--BREAK HABIT--\nPlease enter the id of the habit you would like to delete ('-1' to review records): ");
                        inputID = Console.ReadLine();
                        continue;
                    }
                    Console.Write("\nPlease enter a valid integer ('-1' to review records): ");
                    inputID = Console.ReadLine();
                }

                Console.WriteLine("\n--BREAKING HABIT--");
                habitDatabase.BreakHabit(id);
                Console.WriteLine("\n--HABIT BROKEN. GOING TO MAIN MENU--");
                //BreakHabit()
                break;
            }
    }
}

void ShowInstructions()
{
    Console.WriteLine("  |QUIT: '0'\n  |REVIEW RECORDS: '1'\n  |INSERT HABIT: '2'\n  |UPDATE HABIT: '3'\n  |DELETE HABIT: '4'");

}

Console.WriteLine("---QUTTING APPLICATION---".PadLeft(40));
//Menu 
//Options: 
//Insert a habit
//Get the data
//Update the data
//Delete the data


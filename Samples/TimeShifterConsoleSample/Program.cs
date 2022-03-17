using meliheran;
using System;

namespace TimeShifterSample
{
    class Program
    {
        //Timeshifter will be used for prevent loading data from db on every key press
        static TimeShifter<string> ts = new TimeShifter<string>();
        

        //Sample textbox simulates a text search from database
        static SampleTextBox searchBox = new SampleTextBox();

        static void Main(string[] args)
        {
            //sample text to search
            string searchText = "This is sample text to search for.";
            int i = 0;

            ts.ShiftingTime = 1000 * 2; //if user waits for 2 seconds go and get data for database
            ts.OnEndShiftingTime += Shifter_OnEndShiftingTime;

            //key press events
            searchBox.OnKeyPressed += SearchBox_OnKeyPressed;

            //type some string
            do
            {
                searchBox.Text += searchText[i % searchText.Length];        //get sample text typing
                System.Threading.Thread.Sleep(new Random().Next(1,2400));  //unpredicted key press delays
                
                i++;

            } while (ts.IsShifting);

            Console.ReadLine();
        }

        private async static void SearchBox_OnKeyPressed(string textValue)
        {
            Console.WriteLine($"Key Pressed - '{searchBox.Text}' - Wait user still typing, {ts.GetShiftPoolTime()} millisec left..");

            //Just call Shift method. On every call time will be postpone if calls under x milliseconds.
            await ts.Shift(textValue);

            //No need to call database for searching. Just let user type more text.
        }

        private static void Shifter_OnEndShiftingTime(string obj)
        {
            //go and do your database call !
            Console.WriteLine("----User ended typing----");
            Console.WriteLine($"({ts.ShiftingTime} milliseconds passed for last typing)");
            Console.WriteLine($"Current search text value is '{obj}'");
            Console.WriteLine($"Go and make a db call just for one time not for every key press event..");
        }
    }

    class SampleTextBox
    {
        private string _text;

        public delegate void TextHandler(string textValue);
        public event TextHandler OnKeyPressed;

        public string Text 
        {
            get
            {
                return _text;
            }
            set 
            {
                _text = value;
                if (OnKeyPressed != null) OnKeyPressed(_text);
            }
        }

        public SampleTextBox()
        {
            
        }
    }
}

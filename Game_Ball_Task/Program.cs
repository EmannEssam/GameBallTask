using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Game_Ball_Task
{
    class Program
    { 
        //--------------------------variables------------------------------//
        static int  month;
        static int year;
        static List<String> input_data = new List<String>();
        static List<String> output_cds;
        static List<double> output_values;
        static bool year_check;
        //--------------------------read_input_file------------------------------//
        static void read_input_file()
        {

            string path=   AppDomain.CurrentDomain.BaseDirectory + "input.csv";
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    //--------------profit calculation--------------------//
                    double annual_rate = Convert.ToDouble(values[3]);
                    int amount = Convert.ToInt32(values[4]);
                    String pay_cycle = values[5];
                    double profit = amount * (annual_rate / 100);
                    if (pay_cycle == "monthly")
                        profit = profit / 12;
                    else
                        if (pay_cycle == "quarterly")
                        profit = profit / 4;
                    //------------------insert the profit_value---------------------//
                    line = line.Insert(line.Length, "," + profit);
                    input_data.Add(line);
                }
            }          
        }
        //--------------------------user_console_input------------------------------//
        static void user_console_input()
        {
            Console.WriteLine("input month :");
            month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("input year :");
            year = Convert.ToInt32(Console.ReadLine());    
        }
        //----------------------result_calculation---------------------------------------//
        static void result_calculation()
        {
            year_check = false;
            output_cds = new List<String>();
            output_values = new List<double>();
            //-------------------------calculate_the_expiry_date_ and check if the CD is expired----------------------------------//
            foreach (String cd in input_data)
            {                
                var one_entry = cd.Split(",");
                output_cds.Add(one_entry[0]);
                //--------------------------adjusting_dates------------------------//
                var start_year = Convert.ToInt32(one_entry[1].Split("/")[2]);
                var duration = Convert.ToInt32(one_entry[2]);
                var end_year = start_year + duration;
                var end_month = Convert.ToInt32(one_entry[1].Split("/")[1]);

                //-------------------------profit_calculation----------------------------------//
                if (end_year > year)                
                    output_values.Add(calculation_helper(one_entry[5], Convert.ToDouble(one_entry[6])));          
                else

                  if (end_year == year)
                  {
                    if (month > end_month)                     
                        output_values.Add(0);                    
                    else
                    {
                        if (end_month == month)
                            year_check = true;

                        output_values.Add(calculation_helper(one_entry[5], Convert.ToDouble(one_entry[6])));

                  }

                  }
                else
                    output_values.Add(0);
            }      
        }
        //-----------------------------------display_output--------------------------//
        static void display_output()
        {
            List<String> distinct = output_cds.Distinct().ToList();

            foreach (String cd_number in distinct)
            {
                double output = 0;
                for (int i = 0; i < output_cds.ToArray().Length; i++)
                {
                    if (output_cds[i].Equals(cd_number))
                        output += output_values.ToArray()[i];
                }
                Console.WriteLine(output);
            }
        }
        //--------------------------------------calculation_helper------------------------------------//
        static double calculation_helper(String pay_cycle, double pay)
        { 
            if (pay_cycle == "monthly")
                return pay;
            else
               if (pay_cycle == "quarterly")
                return pay * (month / 3);

            else
                if (pay_cycle == "yearly" && year_check)
                return pay;
            return 0;
        }
        //--------------------------------------main_method------------------------------------//
        static void Main(string[] args)
        {
            read_input_file();
            while (true)
            {
                user_console_input();
                result_calculation();
                display_output();
                Console.WriteLine("To end the program type end otherwise type whatever you want !!");
                if (Console.ReadLine() == "end")
                   break;
            }        
           
        }
    }
}

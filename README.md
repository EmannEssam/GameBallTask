# GameBallTask

This is a console-based application in C# that can be used to calculate the expected income for CDs for a given a month and year (user_input) using an input csv file with the cds data.


The program.cs file has 5 main methods :

1. read_input_file :- this method is used to read the input.csv fileand calculate the interest rate for each record and store the value using the pay cycle as a reference.

2. result_calculation:- the method has 2 main aims first one is to find if the cd already expired (i.e outdated) by checking aganist the date the user enters and then find  it's corresponding earining based on the interest value calculated before and the current_date.

3. calculation_helper:- This method is helper method to find exact interest value per record.

4. Display_output:- Methodd that handles displaying output and aggreagting the earnings of the the same cds(i.e. same number).

5. user_console_input:- This method handles the input from the console (i.e. the input month and year).


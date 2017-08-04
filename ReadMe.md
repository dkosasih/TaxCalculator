Assumption:
- All input CSV have consistent header name.
- IPayCalculator have only one implementation: MonthlyPayCalculator; Otherwise the IoC would be constructed differently.

How to run:
Open the solution file and build the solution for the nuget to get all the necessary library and build.

The .exe file require -i and -o.
-i is the file path where the input CSV provided to the application.
-o is the file path to where the rresult CSV will be saved to.


// See https://aka.ms/new-console-template for more information
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true,

    Delimiter = ","
};

//var streamReader = File.OpenText("emplProjects.csv");
using (var streamReader = new StreamReader("emplProjects.csv"))
{
    using (var csvReader = new CsvReader(streamReader, csvConfig))
    {
        csvReader.Context.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.Add("NULL");

        csvReader.Read();
        csvReader.ReadHeader();

        var empls = csvReader.GetRecords<Employee>();
        var employees = new List<Employee>();

        employees.AddRange(empls);
        var tempPeriod = 0;
        double higherDays = 0;
        int comEmpl1Id = 0;
        int comEmpl2Id = 0;

        int comProjectId = 0;
        double tempTotalDays = 0;
        if (employees.Count() > 0)
        {
            for (int i = 1; i < employees.Count(); i++)
            {
                for (int y = 2; y < employees.Count(); y++)
                {
                    if (int.Parse(employees[i].ProjectID) == int.Parse(employees[y].ProjectID) & (int.Parse(employees[i].EmpID) != int.Parse(employees[y].EmpID)))
                    {
                        DateTime sd1,sd2,fd1,fd2;

                        if (employees[i].DateFrom != "NULL")
                        {
                             sd1 = DateTime.Parse(employees[i].DateFrom);
                        }
                        else
                        {
                             sd1 = DateTime.Now;
                        }

                        if (employees[i].DateTo != "NULL")
                        {
                             fd1 = DateTime.Parse(employees[i].DateTo);
                        }
                        else
                        {
                             fd1 = DateTime.Now;
                        }

                        if (employees[y].DateFrom != "NULL")
                        {
                             sd2 = DateTime.Parse(employees[y].DateFrom);
                        }
                        else
                        { 
                             sd2 = DateTime.Now;
                        }

                        if (employees[y].DateTo != "NULL")
                        {
                            fd2 = DateTime.Parse(employees[y].DateTo);
                        }
                        else
                        { 
                            fd2 = DateTime.Now;
                        }
                        
                        if (CheckCommonDates(sd1, sd2, fd1, fd2))
                        {

                            higherDays = CheckHigherDays(sd1, sd2, fd1, fd2);
                            if (higherDays > 0)
                            {
                                
                                if (higherDays> tempTotalDays)
                                {
                                    tempTotalDays = higherDays;
                                    comProjectId = int.Parse(employees[i].ProjectID);
                                    comEmpl1Id = int.Parse(employees[i].EmpID);
                                    comEmpl2Id = int.Parse(employees[y].EmpID);
                                }
                                
                            }

                        }

                    }
                }


            }
        }
        Console.WriteLine($"{comEmpl1Id}, {comEmpl2Id}, {comProjectId}");
    };
};

double CheckHigherDays(DateTime sd1, DateTime sd2, DateTime fd1, DateTime fd2)
{
    
    double totalDays = 0;
    
    TimeSpan tempDays;
    if (sd1.Date >= sd2.Date && fd1.Date <= fd2.Date)
    {
        tempDays = (sd1 - fd1);
    }
    else if (sd2.Date >= sd1.Date && fd2.Date <= fd1.Date)
    {
        tempDays = (sd2 - fd2);
    }
    else if (sd2.Date >= sd1.Date && fd2.Date >= fd1.Date)
    {
        tempDays = (sd2 - fd1);
    }
    else
    {
        tempDays = (sd1 - fd2);
    }
    if (tempDays.TotalDays<0)
    {
        totalDays = (double)tempDays.Days;
        if (totalDays<0)
        {
            totalDays *= -1;
        }
    }
    return totalDays;
}

static bool CheckCommonDates(DateTime sd1, DateTime sd2, DateTime fd1, DateTime fd2)
{
    TimeSpan tempDays;
    if ((sd2.Date <= fd1.Date) || (sd1.Date <= fd2.Date))
    {
        tempDays = (sd1 - fd2);


        if (tempDays.Days != 0)
        {
            return true;
        }

    }

    return false;
}

record Employee(string EmpID, string ProjectID, string DateFrom, string DateTo);
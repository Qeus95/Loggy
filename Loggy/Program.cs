
// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Program started!");
System.Threading.Thread.Sleep(500);
Console.WriteLine("\n1. Press Shift + Right-click on desired file\n2. Choose Copy as path option.\n3. Now you can paste file path here \n4. Press Enter button to proceed. \n\n Enter file path: ");
System.Threading.Thread.Sleep(1000);
string file_path = @"C:\Users\Linas\Downloads\Holiday project\0908\SEQLOG001.TXT";
string temp1 = "";
string temp2 = "";
string caller = "Record Saved";
int counter = 0;
bool found;
float average = 0;
string line_temp = "";
int sum = 0;
string resultsFilePath = "";

try {
    file_path = Console.ReadLine();
    file_path = file_path.Remove(0, 1);
    file_path = file_path.Remove(file_path.Length - 1, 1);
    File.ReadLines(file_path);
    Console.WriteLine("Path is valid. Log analysis starting!\n");
}
catch {
    Console.WriteLine("ERROR: Bad path inserted. Program closing!");
    Console.ReadKey();
}

Console.WriteLine("File selected: " + file_path);


//Text failo kurimas
//temp2 = Convert.ToString((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
temp2 = Path.GetFileName(file_path);
//sum = temp2.Length;
temp1 = file_path.Remove(file_path.Length - temp2.Length, temp2.Length);
resultsFilePath = temp1+"analysis_results_"+ temp2;


using (FileStream fs = File.Create(resultsFilePath))
{
    AddText(fs, "--------------Results------------- ");

}




System.Threading.Thread.Sleep(500);
/*string dummyLines = "Some text"; 
File.AppendAllLines(@"C:\Users\Linas\Downloads\Holiday project\0908\Results.txt", dummyLines.Split(Environment.NewLine.ToCharArray()).ToList<string>()); */  // For future, pradziai reikia perskaityti logo failus, ir tada galesim viska surasyti i tekstini failiuka



//Išsaugotų įrašų statistika
counter = 0;
caller = "Record Saved";

System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
    counter++;
}
System.Console.WriteLine("\nThere were {0} records saved.", counter);

using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} records saved.", counter);
}

//Kiek gauta gerų atsakymų į įrašus
counter = 0;
caller = "AVL DATA accepted";

System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
        counter++;
}
System.Console.WriteLine("\nThere were {0} positive answers from server for sent records.", counter);

using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} positive answers from server for sent records.", counter);
}


//Rasta assert klaidų
counter = 0;
caller = "Assert";
System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
        counter++;
}

System.Console.WriteLine("\nThere were {0} assert errors.", counter);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} assert errors.", counter);
}

//Rasta crash module printų
counter = 0;
caller = "Crash module";
System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
        counter++;
}
System.Console.WriteLine("\nThere were {0} lines with crash module print. Note for myself: Have to check actual print in log", counter);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} lines with crash module print. Note for myself: Have to check actual print in log", counter);
}

//Rasta BUS FAULT printų
counter = 0;
caller = "BUS FAULT";
System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
        counter++;
}
System.Console.WriteLine("\nThere were {0} FATAL (BUS fault) errors.", counter);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} FATAL (BUS fault) errors.", counter);
}


//Rasta ERROR: m2m printų
counter = 0;
caller = "ERROR: m2m";
System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
        counter++;
}
System.Console.WriteLine("\nThere were {0} lines with \"ERROR: m2m\" print.", counter);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} lines with \"ERROR: m2m\" print.", counter);
}

//Rasta ERROR@ printų
counter = 0; //Matches found
sum = 0; //Line number in this context
caller = "ERROR@";
System.Threading.Thread.Sleep(500);
foreach (string line in System.IO.File.ReadLines(file_path))
{
    sum++;
    found = line.Contains(caller);
    if (found)
    {
        counter++;
        System.Console.WriteLine("\nError found in line {0}: {1}", sum,line);
        /*using (StreamWriter sw = File.AppendText(resultsFilePath))
        {
            sw.WriteLine("\nError found in line {0}: {1}", sum, line);
        }*/
    }
        
}
System.Console.WriteLine("\nThere were {0} ERROR@ prints.", counter);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} ERROR@ prints.", counter);
}


//Vidutinis RSSI
counter = 0;
caller = "Signal rssi";
average = 0;
System.Threading.Thread.Sleep(500);

foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
    {
       
        
        counter++;
        try
        {
            if (Int32.Parse(line.Substring(line.Length - 2)) != 99)
            average = average + Int32.Parse(line.Substring(line.Length - 2));
            
        }
        catch { };
    }
    
}
average = average / counter;
System.Console.WriteLine("\nThere were {0} Signal RSSI lines with total average value of {1}.", counter, average);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("\nThere were {0} Signal RSSI lines with total average value of {1}.", counter, average);
}

//Bendras įrašų dydis
counter = 0;
caller = "Record Size:	";
System.Threading.Thread.Sleep(500);
temp1 = "Bytes";
System.Console.WriteLine("\nTotal data saved calculation is in progress...(might take a while)");
foreach (string line in System.IO.File.ReadLines(file_path))
{
    found = line.Contains(caller);
    if (found)
    {
        line_temp = line;
        try //Bandom nutrinti po viena simboli nuo galo, kol liks pabaigoj tik zodis "Bytes". Tada zinosim +/- kad pries Bytes bus skaicius, kiekis informacijos ir jau ta galesim sumuoti i kruva
        {
            while (temp1 != line_temp.Substring(line_temp.Length - 5))
            {
                temp2 = line_temp.Remove(line_temp.Length - 1, 1);
                line_temp = temp2;
            }
            line_temp = line_temp.Remove(line_temp.Length - 6, 6);

        }
        catch {
            
           
        };
        try {
            sum = sum + Int32.Parse(line_temp.Substring(line_temp.Length - 4));
           
        }
        catch {
            try
            {
                sum = sum + Int32.Parse(line_temp.Substring(line_temp.Length - 3));
               
            }
            catch
            {
                try
                {
                    sum = sum + Int32.Parse(line_temp.Substring(line_temp.Length - 2));
                   
                }
                catch
                {
                    System.Console.WriteLine("#{0} result is in incorrect format. Could not parse.",counter);
                    using (StreamWriter sw = File.AppendText(resultsFilePath))
                    {
                        sw.WriteLine("#{0} result is in incorrect format. Could not parse.", counter);
                    }
                }
            }

            }

        counter++;
    }

}
System.Console.WriteLine("Total data size of saved records: {0} bytes",sum);
using (StreamWriter sw = File.AppendText(resultsFilePath))
{
    sw.WriteLine("Total data size of saved records: {0} bytes", sum);
}
System.Console.WriteLine("\nExecuted successfully. Press any key to exit application");
Console.ReadKey();


 static void AddText(FileStream fs, string value)
{
    byte[] info = new UTF8Encoding(true).GetBytes(value);
    fs.Write(info, 0, info.Length);
}
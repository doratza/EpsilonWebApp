using consoleApp;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Press one(1) for Employe and two(2) fro manager");

var person=Console.ReadLine();
IPErson persontobe;

if(person=="1")
{
    persontobe=new Employee();
    persontobe.Name="Jim";
    
}
else
{
    persontobe= new Manager();
    persontobe.Name="Taylor";
}

Console.WriteLine("name is " +persontobe.Name);




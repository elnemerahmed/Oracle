using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Threading;

namespace Oracle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "OracleDB Helper";
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-    Visit Ahmed at https://github.com/elnemerahmed");
            Console.ForegroundColor = ConsoleColor.White;
            if (IsUserAdministrator())
            {
                while (true)
                {
                    Console.Write("-    Root@root# Iam Listening: ");
                    string input = Console.ReadLine();
                    if (input.ToLower().Equals("start"))
                    {
                        try
                        {
                            StartService("OracleXEClrAgent");
                            StartService("OracleXETNSListener");
                            StartService("OracleServiceXE");
                            Console.Write("         Just a moment ");
                            for (int i = 0; i < 5; i++)
                            {
                                Thread.Sleep(1000);
                                Console.Write(".");
                            }
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("         Services are started");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("         I think that the services are alreading running, try to stop them instead");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else if (input.ToLower().Equals("stop"))
                    {
                        try
                        {
                            StopService("OracleXEClrAgent");
                            StopService("OracleXETNSListener");
                            StopService("OracleServiceXE");
                            Console.Write("         Just a moment ");
                            for (int i = 0; i < 5; i++)
                            {
                                Thread.Sleep(1000);
                                Console.Write(".");
                            }
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("         Services are stopped");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("         I think that the services are alreading stopped, try to run them instead");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else if (input.ToLower().Equals("exit"))
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-    Sorry, you must open this app as an Admin");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            }
        }


        public static void StartService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            service.Start();
        }
        public static void StopService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            service.Stop();
        }
        public static bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

    }
}

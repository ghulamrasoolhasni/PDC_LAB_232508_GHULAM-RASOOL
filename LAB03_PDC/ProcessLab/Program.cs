using System;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--child")
        {
            RunAsChild();
        }
        else
        {
            RunAsParent();
        }
    }
    static void RunAsChild()
    {
        // Print child PID
        Console.WriteLine($"[Child] PID = {Environment.ProcessId}");       
        // Initialize counter and modify
        int counter = 100;
        counter += 50;
        Console.WriteLine($"[Child] final counter = {counter}");
    }
    static void RunAsParent()
    {
        // Print parent PID
        Console.WriteLine($"[Parent] PID = {Environment.ProcessId}");
        // Initialize counter and modify (+1)
        int counter = 100;
        counter += 1;
        // Path to the currently running executable
        string executablePath = Environment.ProcessPath!;
        // Configure child process startup
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add("--child");
        // Launch child process and wait for completion
        using (Process? childProcess = Process.Start(startInfo))
        {
            childProcess?.WaitForExit();
        }
        // Print final parent counter and explicit address space statement
        Console.WriteLine($"[Parent] final counter = {counter}");
        Console.WriteLine("[Parent] Parent and child counters were modified independently (separate address spaces).");
    }
}
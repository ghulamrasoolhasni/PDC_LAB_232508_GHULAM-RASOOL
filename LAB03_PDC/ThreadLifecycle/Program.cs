using System;
using System.Threading;

class Program
{
    static void Worker()
    {
        Thread.Sleep(200);
    }
    static void Main()
    {
        Thread t = new Thread(Worker);
        // 1. Immediately after creation (Unstarted)
        Console.WriteLine($"After creation: {t.ThreadState}"); 
        // Conceptually: New
        t.Start();       
        // 2. Immediately after Start()
        Console.WriteLine($"Immediately after Start(): {t.ThreadState}"); 
        // Conceptually: Runnable / Ready (or Running)
        Thread.Sleep(50); // Allow thread time to enter Thread.Sleep(200)   
        // 3. While worker is sleeping (WaitSleepJoin)
        Console.WriteLine($"While worker is sleeping: {t.ThreadState}"); 
        // Conceptually: Blocked / Waiting
        t.Join();  
        // 4. After thread execution finishes (Stopped)
        Console.WriteLine($"After Join() completes: {t.ThreadState}"); 
        // Conceptually: Terminated
    }
}
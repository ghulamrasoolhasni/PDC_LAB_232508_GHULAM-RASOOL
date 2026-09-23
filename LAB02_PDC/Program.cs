// lab task: Task 2
using System;
using System.Threading;

class Program
{
    // Worker method executed by each thread
    static void Worker(object? arg)
    {
        int id = (int)arg!;
        // Get the logical CPU on which this thread is currently running
        int cpu = Thread.GetCurrentProcessorId();
        Console.WriteLine(
            $"Thread {id}: running on logical CPU {cpu}"
        );
    }

    static void Main()
    {
        // Detect the number of logical processors
        int numCores = Environment.ProcessorCount;
        Console.WriteLine($"Detected logical cores: {numCores}");
        // Create an array dynamically based on detected processors
        Thread[] threads = new Thread[numCores];
        // Create and start exactly one thread per logical processor
        for (int i = 0; i < numCores; i++)
        {
            // Give each thread its own index
            int idx = i;
            threads[i] = new Thread(() => Worker(idx));
            threads[i].Start();
        }
        // Wait for every thread to finish
        for (int i = 0; i < numCores; i++)
        {
            threads[i].Join();
        }
        // Confirm that all threads have completed
        Console.WriteLine($"All {numCores} threads completed.");
        // Display total number of threads created
        Console.WriteLine($"Total threads created: {threads.Length}");
    }
}
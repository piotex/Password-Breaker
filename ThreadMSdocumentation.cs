using System;
using System.Threading;

public class Example
{
    private static EventWaitHandle ewh;
    private static long threadCount = 0;
    private static EventWaitHandle clearCount = new EventWaitHandle(false, EventResetMode.AutoReset);

    [MTAThread]
    public static void Main()
    {
        ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        for (int i = 0; i <= 4; i++)
        {
            Thread t = new Thread( new ParameterizedThreadStart(ThreadProc) );
            t.Start(i);
        }
        while (Interlocked.Read(ref threadCount) < 5)
        {
            Thread.Sleep(500);
        }
        while (Interlocked.Read(ref threadCount) > 0)
        {
            Console.WriteLine("Press ENTER to release a waiting thread.");
            Console.ReadLine();
            WaitHandle.SignalAndWait(ewh, clearCount);
        }
        Console.WriteLine();
        ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
        for(int i=0; i<=4; i++)
        {
            Thread t = new Thread(
                new ParameterizedThreadStart(ThreadProc)
            );
            t.Start(i);
        }
        while (Interlocked.Read(ref threadCount) < 5)
        {
            Thread.Sleep(500);
        
        Console.WriteLine("Press ENTER to release the waiting threads.");
        Console.ReadLine();
        ewh.Set();
    }

    public static void ThreadProc(object data)
    {
        int index = (int) data;

        Console.WriteLine("Thread {0} blocks.", data);
        // Increment the count of blocked threads.
        Interlocked.Increment(ref threadCount);

        // Wait on the EventWaitHandle.
        ewh.WaitOne();

        Console.WriteLine("Thread {0} exits.", data);
        // Decrement the count of blocked threads.
        Interlocked.Decrement(ref threadCount);

        // After signaling ewh, the main thread blocks on
        // clearCount until the signaled thread has 
        // decremented the count. Signal it now.
        //
        clearCount.Set();
    }
}








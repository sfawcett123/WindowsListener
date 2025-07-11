using System.Diagnostics;
using ThreadState = System.Diagnostics.ThreadState;

namespace Broadcast
{
    public class SimProcess : Process
    {
        // Fix for CS1519 and CS8124: Correctly define the method signature and remove invalid tokens.
        public bool GetProcess(string Name)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == Name)
                {
                    // Process found, return true
                    Debug.WriteLine($"Found process: {process.ProcessName} Status {process.Id}");
                    if( IsRunning(process) == true)
                    {
                        Debug.WriteLine($"Process {process.ProcessName} is running.");
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public static bool IsRunning(Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                Debug.WriteLine($"NOT RUNNING process: {process.ProcessName}");
                return false;
            }

            if ( process.Threads.Count == 0 )
            {
                Debug.WriteLine($"No threads found in process: {process.ProcessName}");
                return false;
            }

            Debug.WriteLine($"{process.Threads.Count} Running threads found in process: {process.ProcessName}");
            return true;
        }
    }
}


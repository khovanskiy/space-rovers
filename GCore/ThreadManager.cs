using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
namespace Game.GCore
{
    public class ThreadManager
    {
        public static void addTask(Runnable task)
        {
            Thread thread = new Thread(task.run);
            thread.Start();
        }
    }
}

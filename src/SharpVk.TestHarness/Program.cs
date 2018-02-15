using SharpVk.Glfw;
using System;
using System.Diagnostics;

namespace SharpVk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run();
        }

        private unsafe void Run()
        {
            try
            {
                Glfw3.Init();

                var callback = new CharDelegate((handle, codepoint) => Console.WriteLine(codepoint));
                
                using (var window = new Window(1920, 1080, "Test"))
                {
                    Glfw3.SetCharCallback(window.Handle, callback);

                    InputAction previous = InputAction.Release;

                    while (!window.ShouldClose)
                    {
                        InputAction mouseAction = Glfw3.GetMouseButton(window.Handle, 1);

                        if (mouseAction != previous)
                        {
                            Console.WriteLine(mouseAction);

                            previous = mouseAction;
                        }

                        Glfw3.PollEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                Glfw3.Terminate();
            }
        }
    }
}

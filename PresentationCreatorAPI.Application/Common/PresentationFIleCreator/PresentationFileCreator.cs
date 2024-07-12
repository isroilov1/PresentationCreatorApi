using Python.Runtime;
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers
{
    public class PresentationFileCreator
    {
        //public static async Task<string> CreatePresentation(PresentationEntity presentationEntity)
        //{
            
        //}

        public static async Task<string> CreatePresentation(PresentationEntity presentationEntity)
        {
            // Python DLL joylashgan yo'lni ko'rsating
            Runtime.PythonDLL = @"C:\Users\Victus\AppData\Local\Programs\Python\Python311\python311.dll";
            PythonEngine.Initialize();
            string scriptName = "createpresentationfile";

            using (Py.GIL())
            {
                // Python skripti joylashgan yo'lni `sys.path` ga qo'shing
                dynamic sys = Py.Import("sys");
                sys.path.append(@"D:\Dot net Projects\github\PresentationCreatorAPI\Python"); // Bu yo'lni o'zingizning yo'lingizga o'zgartiring

                // Python skriptini import qiling va funksiyani chaqirish
                dynamic pythonScript = Py.Import(scriptName);
                dynamic result = pythonScript.run_async_function(presentationEntity);
                return result;
            }
        }
    }
}

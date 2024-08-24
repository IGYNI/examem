using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageDownloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                int enter = 0;

                Console.WriteLine($"Скачать Одно фото(1) \n"+
                                  $"Скачать Список фото(2)");
                
                enter = Convert.ToInt32(Console.ReadLine());

                switch (enter)
                {
                    case 1:
                        await DownloadImagesAsync();
                        break;
                    case 2:
                        await DownloadAndSaveImagesAsync();
                        break;

                    

                }



            }
            
        }

        static async Task DownloadImagesAsync()
        {
            Console.WriteLine("ССЫЛКА:");
            string url = Console.ReadLine();
            Console.WriteLine("ПУТЬ (без имени):");
            string outputPath = Console.ReadLine() + "\\";
            Console.WriteLine("ИМЯ:");
            outputPath += Console.ReadLine() + ".jpg";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(url);

                    await File.WriteAllBytesAsync(outputPath, imageBytes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка при скачивании изображения: " + ex.Message);
                }
            }

            Console.WriteLine("Изображение успешно скачано и сохранено в " + outputPath);
        }

        static async Task DownloadAndSaveImagesAsync()
        {
            int i = 0;
            List<string> Urls = new List<string>();
            while(true)
            {
                i++;
                Console.WriteLine($"ССЫЛКА НОМЕР {i} (для окончания напишите n):");
                string _url = Console.ReadLine();
                if(_url == "n") break;
                Urls.Add(_url);
            }
            Console.WriteLine("ПУТЬ ДЛЯ ИЗОБРАЖЕНИЙ (имя мы сами выберем, соре👉🏿👈🏿):");
            string outputPath = Console.ReadLine();
            int j = 0;
            foreach (var url in Urls)
            {
                j++;
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        byte[] imageBytes = await client.GetByteArrayAsync(url);

                        await File.WriteAllBytesAsync(outputPath + $"\\file({j}).jpg", imageBytes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка при скачивании изображения: " + ex.Message);
                    }
                }
                Console.WriteLine("Изображение успешно скачано и сохранено в " + outputPath);
            }
            

            
        }


    }
}

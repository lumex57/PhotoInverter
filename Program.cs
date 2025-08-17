using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main(string[] args)

    {
        string[] ImageExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".tiff" };
        
        if (args.Length == 0) {
            Console.WriteLine("[INFO]");
            Console.WriteLine("Please Drag and drop your image file onto this executable.");
            exit();
        }

        foreach (var file in args) {
            if (File.Exists(file)) { 
                string ext = Path.GetExtension(file).ToLower();

                if (ImageExtensions.Contains(ext))
                {
                    try
                    {
                        Bitmap original = new Bitmap(file);
                        Bitmap inverted = new Bitmap(original.Width, original.Height);

                        for (int y = 0; y < original.Height; y++) { 
                            for (int x = 0; x < original.Width; x++)
                            {
                                Color pixel = original.GetPixel(x, y);
                                Color invertedPixel = Color.FromArgb(
                                    pixel.A,

                                    255 - pixel.R,

                                    255 - pixel.G,

                                    255 - pixel.B
                                );

                                inverted.SetPixel(x, y, invertedPixel);
                            }
                        }

                        string dir = Path.GetDirectoryName(file);
                        string filename = Path.GetFileNameWithoutExtension(file);

                        string outputPath = Path.Combine(dir, filename + "_inverted" + ext);
                        inverted.Save(outputPath, ImageFormat.Png);


                    } catch (Exception ex)
                    {

                        Console.WriteLine("[ERROR] failed to process.");
                        Console.WriteLine(ex.Message);
                        exit();

                    }
                }
                else {
                    Console.WriteLine("[INFO]");
                    Console.WriteLine("Please Drag and drop your image file onto this executable.");
                    exit();
                }
            }
        }
    }

    static void exit()
    {
        Console.Write("Press any key to exit..");
        Console.ReadKey();
    }
}

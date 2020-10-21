using ItemPlacementKnowlegeBase.Images;
using ItemPlacementKnowlegeBase.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItemPlacementKnowlegeBase.Gui
{
    class TextureResource
    {
        private int size = KnowlegeBaseManager.get().LoadField(true).CellSize;
        //private static TextureResource instance;

        private static List<TextureResource> listRes = new List<TextureResource>();
        protected TextureResource(string name)
        {
            this.Name = name;
            //this.SourceName = path;
            Load();
        }

        public Bitmap GetBitmap()
        {
            return this.Source;
        }
        public String Name { get; private set; }

       // public String SourceName { get; set; }

        public Bitmap Source { get; set; }

        public int TextureId { get; set; }

        public static TextureResource get(string name)
        {
            if (listRes.Find(x => x.Name == name) == null)
            {
                listRes.Add(new TextureResource(name));
                //instance = new TextureResource(name);
                Console.WriteLine("Create new instance ", name);
            }
            else
            {
                Console.WriteLine("Get existing instance ", name);
            }
            return listRes.Find(x => x.Name == name);
        }

        public override string ToString()
        {
            return "Name: " + Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            TextureResource objAsPart = obj as TextureResource;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public override int GetHashCode()
        {
            return TextureId;
        }
        public bool Equals(TextureResource other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        static int FileIsOk(string nameFile)
        {
            if(! (File.Exists(nameFile)))
            {
                Console.WriteLine("Image file not exist");
                return 0;
            }
            else
            {
                try
                {
                    Bitmap bitmap = new Bitmap(nameFile);
                    bitmap.Dispose();
                    return 1;
                }
                catch
                {
                    Console.WriteLine("File is not image");
                    return -1;
                }
            }
        }
        public void Load(string source)
        {
            this.Source.Dispose();
            int checkFile = FileIsOk(source);
            if (checkFile == 1)
            {
                this.Source = ResizeImage(new Bitmap(ImagePathes.PREFIX + this.Name + ".jpg"),  size, size);
                Console.WriteLine("Image loaded");
            }
            else
            {
                var size = this.size;
                var cells = 3;
                var bitmap = new Bitmap(size, size);
                var cellSize = size / cells;
                Graphics gr = Graphics.FromImage(bitmap);
                for (int i = 0; i <= cells; i++)
                    for (int j = 0; j <= cells; j++)
                    {
                        var brush = (i % 2 == j % 2) ? Brushes.Red : Brushes.Yellow;
                        gr.FillRectangle(brush, i * cellSize, j * cellSize, cellSize, cellSize);
                    }
                this.Source = (Bitmap)bitmap.Clone();
                bitmap.Dispose();
            }
        }

        public void Load()
        {
            if(this.Source != null)
                this.Source.Dispose();

            string fullFilename = this.Name;
            int checkFile = FileIsOk(fullFilename);
            if (checkFile == 1)
            {
                this.Source = ResizeImage( new Bitmap(fullFilename), size, size);
                Console.WriteLine("Image loaded");
            }
            else
            {
                var size = this.size;
                var cells = 3;
                var bitmap = new Bitmap(size, size);
                var cellSize = size / cells;
                Graphics gr = Graphics.FromImage(bitmap);
                for (int i = 0; i <= cells; i++)
                    for (int j = 0; j <= cells; j++)
                    {
                        var brush = (i % 2 == j % 2) ? Brushes.Red : Brushes.Yellow;
                        gr.FillRectangle(brush, i * cellSize, j * cellSize, cellSize, cellSize);
                    }
                this.Source = (Bitmap)bitmap.Clone();
                bitmap.Dispose();
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}

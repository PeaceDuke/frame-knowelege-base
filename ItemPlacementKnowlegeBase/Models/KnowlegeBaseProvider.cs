using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase
{
    class KnowlegeBaseProvider
    {
        private static KnowlegeBaseProvider instance;
        private static List<FilledCell> filledCells;
        private KnowlegeBaseProvider()
        {
            //тут создается база знаний, пока это просто вектор говна
            filledCells = new List<FilledCell>();
        }

        public static KnowlegeBaseProvider get()
        {
            if (instance == null)
                instance = new KnowlegeBaseProvider();

            return instance;
        }

        //Для рендеринга мне нужно знать, что рисовать. Вызывается каждый draw call.
        public List<FilledCell> getFramesToDraw()
        {
            return filledCells;
        }
        public void removeFrame(int cellX, int cellY)
        {
            //тут фрейм должен быть удален из базы знаний
            filledCells.RemoveAll(cell => cell.x == cellX && cell.y == cellY);
        }

        public void addFrame(string draggedData, int cellX, int cellY)
        {
            //тут фрейм должен добавиться в базу знатий
            filledCells.Add(new FilledCell(cellX, cellY, Main_form.draggedData));
        }

        public class FilledCell
        {
            //координаты в сетке. (0,0) - левый верхний угол
            public int x, y;
            public Bitmap bitmap;
            public string name;

            public FilledCell(int x, int y, string name, Bitmap bitmap = null)
            {
                this.x = x;
                this.y = y;
                this.bitmap = bitmap;
                this.name = name;
            }
        }
    }
}

using ItemPlacementKnowlegeBase.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    public interface IKnowlegeBaseProvider
    {

        /// <summary>
        /// Загружает из БЗ соответствующий фрейм и создает описание для поля
        /// </summary>
        /// <returns>Параметры генерируемого поля</returns>
        Field LoadField();

        /// <summary>
        /// Загружает из БЗ список доступных предметов
        /// </summary>
        /// <returns>Список доступных предметов</returns>
        List<Item> LoadItems();

        /// <summary>
        /// Место где происходит вся магия, внутри происходит проверка на распологаемость предмета, 
        /// при успешном добавлении, БЗ обновляется
        /// </summary>
        /// <param name="cell">Клетка в которую пытается поместиться предмет</param>
        /// <param name="item">Предмет который пытаются поместить</param>
        /// <returns>Помещен ли предмет?</returns>
        bool PlaceItem(Cell cell, Item item);

        /// <summary>
        /// Место где происходит вся магия, внутри происходит проверка на распологаемость предмета, 
        /// при успешном удалении, БЗ обновляется
        /// </summary>
        /// <param name="cell">Клетка из которой пытаются достать предмет</param>
        /// <returns>Помещен ли предмет?</returns>
        bool RemoveItem(Cell cell);

        /// <summary>
        /// Тут будет объяснение того, почему предмет нельзя поместить
        /// </summary>
        /// <returns>Строки объяснения со сработавшими фреймами</returns>
        List<string> GetLastReasoning();
    }
}

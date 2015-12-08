## TimeSeriesPrediction
WinForms - приложение для прогноза временных рядов.

####Алгоритм работы.

1) Получение исходных данных из указаного источника. Конвертация и сохранение данных для дальнейшей работы

2) Выбор из исходных данных периода, данные из которого будут использоваться как основа для прогноза. Выбор периода прогноза.

3) Прогнозирование с использованием существующих методов ( наивный байесовсий классификатор, simple moving average, exponential smoothing)

4) Вычисление ошибки для каждого из методов.

5) Отрисовка графиков.


####Реализация
##### 1.Получение исходных данных
Ссылка на исходный json находится в ``QueryCommand.cs``, строчка ``public static string UrlUSDEURCurrencyJSON``

Данные помещаются в структуру данных ``Dictionary<string,string>``. 

А так же записываются в базу. ``DbProvider.cs`` - класс, инкапсулирующий работу с базой

За это отвечает метод ``public static IDictionary<string,string> GetDataFromJson()`` класса ``DataHelper.cs``

##### 2. Выбор периодов исходных данных и прогноза.

Необходимо реализовать. Имеются элементы (выпадающие списки, check-box-ы, button), **необходимо реализовать обработчики**.

##### 3-4. Прогнозирование и подсчет ошибки.
Методы прогноза и подсчета ошибки находятся в классе ``TimeSeries.cs``. Результат работы реализованных методов помещается в объект класса ``ForecastTable.cs``

##### 5. Отрисовка
Для отрисовки используется библиотека ``OxyPlot - http://docs.oxyplot.org/``. 
Для преобразования данных, полученных с помощью методов прогноза используются методы, реализованные в классе ``DataHelper.cs``
**Необходимо дореализовать** (связать обработчики UI-элементов(кнопки, чекбоксы) с отрисовкой соответствующих элементов.






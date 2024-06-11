using System;
using System.IO;
using ClosedXML.Excel;

Console.Write("Введите число элементов: ");
if (!int.TryParse(Console.ReadLine(), out int numberOfElements) || numberOfElements < 1)
{
    Console.WriteLine("Некорректный ввод. Введите положительное целое число.");
    return;
}

var truthTable = GenerateTruthTable(numberOfElements);

using (var workbook = new XLWorkbook())
{
    var worksheet = workbook.Worksheets.Add("Таблица истинности");

    for (int i = 0; i < truthTable.GetLength(0); i++)
    {
        for (int j = 0; j < truthTable.GetLength(1); j++)
            worksheet.Cell(i + 1, j + 1).Value = truthTable[i, j];
    }

    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    string fileName = $"TruthTable_{GetRandomString()}.xlsx";
    string filePath = Path.Combine(desktopPath, fileName);

    workbook.SaveAs(filePath);
    Console.WriteLine($"Таблица истинности сохранена в файл {fileName}");
}


static int[,] GenerateTruthTable(int numberOfElements)
{
    int numberOfRows = (int)Math.Pow(2, numberOfElements);
    int[,] truthTable = new int[numberOfRows, numberOfElements];

    for (int i = 0; i < numberOfRows; i++)
    {
        for (int j = 0; j < numberOfElements; j++)
            truthTable[i, j] = (i >> (numberOfElements - j - 1)) & 1;
    }

    return truthTable;
}
static string GetRandomString()
{
    string path = Path.GetRandomFileName();
    path = path.Replace(".", "").Remove(6);
    return path;
}
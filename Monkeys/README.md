Приложение Monkeys сделано на базе .NET версии 6.0, а также фреймворка Avalonia.
Здесь представлен список NuGet пакетов, используемых в приложении Monkeys:
1. Microsoft.EntityFrameworkCore (v. 7.0.5)
2. Microsoft.EntityFrameworkCore.Tools (v. 7.0.5)
3. Microsoft.EntityFrameworkCore.Design (v. 7.0.5)
4. Npgsql.EntityFrameworkCore.PostgreSQL (v. 7.0.4)
5. Avalonia.Controls.DataGrid (v. 0.10.21)
6. MessageBox.Avalonia (v. 2.0.0)

Строка для реализации реконструирования:
Scaffold-DbContext "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123" -Provider Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -Schema "Monkeys" -Context "monkeysContext"

!!!Для оптимальной работы приложения в СУБД необходимо выполнить скрипт, представленный в файле mnkSQLscript.sql в каталоге данного репозитория, предварительно создав схему Monkeys. Если схема называется по-другому - изменить строку реконструирования с подходящим названием!!!
